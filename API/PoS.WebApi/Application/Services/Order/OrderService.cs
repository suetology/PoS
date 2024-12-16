using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Services.Reservation;
using PoS.WebApi.Domain.Enums;
namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Application.Services.Customer;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;
using PoS.WebApi.Application.Services.Customer.Contracts;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Application.Services.Service.Contracts;
using PoS.WebApi.Application.Services.Tax.Contracts;
using PoS.WebApi.Application.Services.Discount.Contracts;

public class OrderService: IOrderService
{
    private readonly IReservationService _reservationService;
    private readonly ICustomerService _customerService;
    private readonly IItemService _itemService;
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IItemVariationRepository _itemVariationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IReservationService reservationService, 
        ICustomerService customerService,
        IItemService itemService,
        IOrderRepository orderRepository,
        IItemRepository itemRepository,
        IItemVariationRepository itemVariationRepository, 
        IUnitOfWork unitOfWork) 
    {
        _reservationService = reservationService;
        _customerService = customerService;
        _itemService = itemService;
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _itemVariationRepository = itemVariationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateOrder(CreateOrderRequest request)
    {
        if (request.CustomerId == null)
        {
            request.Customer.BusinessId = request.BusinessId;

            var customerResponse = await _customerService.CreateCustomer(request.Customer);
            request.CustomerId = customerResponse.Id;
        }

        var orderItems = await Task.WhenAll(request.OrderItems.Select(async o => {
                var orderItem = new OrderItem
                {
                    BusinessId = request.BusinessId,
                    Quantity = o.Quantity,
                    ItemId = o.ItemId
                };

                var item = await _itemRepository.Get(o.ItemId);

                await _itemService.ChangeItemStock(new ChangeItemStockRequest
                {
                    ItemId = item.Id,
                    BusinessId = request.BusinessId,
                    StockChange = -o.Quantity
                });

                foreach (var itemVariationId in o.ItemVariationsIds)
                {
                    var itemVariation = await _itemVariationRepository.Get(itemVariationId);
                    
                    orderItem.ItemVariations.Add(itemVariation);

                    await _itemService.ChangeItemVariationStock(new ChangeItemVariationStockRequest
                    {
                        ItemVariationId = itemVariation.Id,
                        BusinessId = request.BusinessId,
                        StockChange = -o.Quantity
                    });
                }

                return orderItem;
            }));
        
        var order = new Order
        {   
            BusinessId = request.BusinessId,
            EmployeeId = request.EmployeeId,
            CustomerId = request.CustomerId.Value,
            ServiceChargeId = request.ServiceChargeId,
            Status = OrderStatus.Open,
            Created = DateTime.UtcNow,
            OrderItems = orderItems,
            TipAmount = 0
        };
        
        await _orderRepository.Create(order);
        await _unitOfWork.SaveChanges();
        
        if (request.Reservation != null)
        {
            request.Reservation.BusinessId = order.BusinessId;
            request.Reservation.OrderId = order.Id;
        
            await _reservationService.CreateReservation(request.Reservation);
        }
    }

    public async Task<GetAllOrdersResponse> GetAllOrders(GetAllOrdersRequest request)
    {
        var orders = await _orderRepository.GetAllFiltered(request.QueryParameters);
        var orderDtos = orders
            .Where(o => o.BusinessId == request.BusinessId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                Status = o.Status,
                Created = o.Created,
                Closed = o.Closed,
                FinalAmount = o.CalculateTotalAmout(),
                PaidAmount = o.CalculatePaidAmount(),
                TipAmount = o.TipAmount,
                Employee = new UserDto
                {
                    Id = o.Employee.Id,
                    Username = o.Employee.Username,
                    Role = o.Employee.Role
                },
                Customer = new CustomerDto
                {
                    Id = o.Customer.Id,
                    Name = o.Customer.Name,
                    Email = o.Customer.Email,
                    PhoneNumber = o.Customer.PhoneNumber
                },
                ServiceCharge = o.ServiceCharge == null ? null : new ServiceChargeDto
                {
                    Id = o.ServiceCharge.Id,
                    Name = o.ServiceCharge.Name,
                    Description = o.ServiceCharge.Description,
                    Value = o.ServiceCharge.Value,
                    IsPercentage = o.ServiceCharge.IsPercentage
                },
                OrderItems = o.OrderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    Item = new ItemDto
                    {
                        Id = i.Item.Id,
                        Name = i.Item.Name,
                        Price = i.Item.Price,
                        Taxes = i.Item.Taxes.Select(t => new TaxDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Value = t.Value,
                            Type = t.Type,
                            IsPercentage = t.IsPercentage
                        }).ToList(),
                        ItemGroup = new ItemGroupDto
                        {
                            Id = i.Item.ItemGroup.Id,
                            Name = i.Item.ItemGroup.Name,
                            Discounts = i.Item.ItemGroup.Discounts.Select(d => new DiscountDto
                            {
                                Id = d.Id,
                                Name = d.Name,
                                IsPercentage = d.IsPercentage,
                                Value = d.Value
                            }).ToList()
                        },
                        Discounts = i.Item.Discounts.Select(d => new DiscountDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            IsPercentage = d.IsPercentage,
                            Value = d.Value
                        }).ToList()
                    },
                    ItemVariations = i.ItemVariations.Select(v => new ItemVariationDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        AddedPrice = v.AddedPrice
                    })
                }),
                Reservation = o.Reservation == null ? null : new ReservationDto
                {
                    Id = o.Reservation.Id,
                    Status = o.Reservation.Status,
                    AppointmentTime = o.Reservation.AppointmentTime,
                    Service = new ServiceDto
                    {
                        Id = o.Reservation.Service.Id,
                        Name = o.Reservation.Service.Name,
                        Price = o.Reservation.Service.Price,
                        Duration = o.Reservation.Service.Duration
                    }
                },
                Discount = o.Discount == null ? null : new DiscountDto
                {
                    Id = o.Discount.Id,
                    Name = o.Discount.Name,
                    Value = o.Discount.Value,
                    IsPercentage = o.Discount.IsPercentage
                }
                // add paymentDtos
                // add refundDto
            });

        return new GetAllOrdersResponse
        {
            Orders = orderDtos
        };
    }

    public async Task<GetOrderResponse> GetOrder(GetOrderRequest request)
    {
        var order = await _orderRepository.Get(request.Id);

        if (order.BusinessId != request.BusinessId)
        {
            return null;
        }

        return new GetOrderResponse
        {
            Order = new OrderDto
            {
                Id = order.Id,
                Status = order.Status,
                Created = order.Created,
                Closed = order.Closed,
                FinalAmount = order.CalculateTotalAmout(),
                PaidAmount = order.CalculatePaidAmount(),
                TipAmount = order.TipAmount,
                Employee = new UserDto
                {
                    Id = order.Employee.Id,
                    Username = order.Employee.Username,
                    Role = order.Employee.Role
                },
                Customer = new CustomerDto
                {
                    Id = order.Customer.Id,
                    Name = order.Customer.Name,
                    Email = order.Customer.Email,
                    PhoneNumber = order.Customer.PhoneNumber
                },
                ServiceCharge = order.ServiceCharge == null ? null : new ServiceChargeDto
                {
                    Id = order.ServiceCharge.Id,
                    Name = order.ServiceCharge.Name,
                    Description = order.ServiceCharge.Description,
                    Value = order.ServiceCharge.Value,
                    IsPercentage = order.ServiceCharge.IsPercentage
                },
                OrderItems = order.OrderItems.Select(i => new OrderItemDto
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    Item = new ItemDto
                    {
                        Id = i.Item.Id,
                        Name = i.Item.Name,
                        Price = i.Item.Price,
                        Taxes = i.Item.Taxes.Select(t => new TaxDto
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Value = t.Value,
                            Type = t.Type,
                            IsPercentage = t.IsPercentage
                        }).ToList(),
                        ItemGroup = new ItemGroupDto
                        {
                            Id = i.Item.ItemGroup.Id,
                            Name = i.Item.ItemGroup.Name,
                            Discounts = i.Item.ItemGroup.Discounts.Select(d => new DiscountDto
                            {
                                Id = d.Id,
                                Name = d.Name,
                                IsPercentage = d.IsPercentage,
                                Value = d.Value
                            }).ToList()
                        },
                        Discounts = i.Item.Discounts.Select(d => new DiscountDto
                        {
                            Id = d.Id,
                            Name = d.Name,
                            IsPercentage = d.IsPercentage,
                            Value = d.Value
                        }).ToList()
                    },
                    ItemVariations = i.ItemVariations.Select(v => new ItemVariationDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        AddedPrice = v.AddedPrice
                    })
                }),
                Reservation = order.Reservation == null ? null : new ReservationDto
                {
                    Id = order.Reservation.Id,
                    Status = order.Reservation.Status,
                    AppointmentTime = order.Reservation.AppointmentTime,
                    Service = new ServiceDto
                    {
                        Id = order.Reservation.Service.Id,
                        Name = order.Reservation.Service.Name,
                        Price = order.Reservation.Service.Price,
                        Duration = order.Reservation.Service.Duration
                    }
                },
                Discount = order.Discount == null ? null : new DiscountDto
                {
                    Id = order.Discount.Id,
                    Name = order.Discount.Name,
                    Value = order.Discount.Value,
                    IsPercentage = order.Discount.IsPercentage
                }
                // add paymentDtos
                // add refundDto
            }
        };
    }

public async Task<bool> UpdateOrder(UpdateOrderRequest request)
{
    var existingOrder = await _orderRepository.Get(request.Id);

    if (existingOrder == null || existingOrder.BusinessId != request.BusinessId)
    {
        throw new KeyNotFoundException("Order not found.");
    }

    var oldItemsDict = existingOrder.OrderItems.ToDictionary(oi => oi.ItemId);

    var newOrderItems = new List<OrderItem>();

    foreach (var requestOrderItem in request.OrderItems)
    {
        var itemId = requestOrderItem.ItemId;
        var quantity = requestOrderItem.Quantity;

        var item = await _itemRepository.Get(itemId);

        var oldOrderItem = oldItemsDict.GetValueOrDefault(itemId);
        var currentOrderQuantity = oldOrderItem?.Quantity ?? 0;
        var quantityDifference = quantity - currentOrderQuantity;

        if (quantityDifference != 0)
        {
            if (quantityDifference > 0 && item.Stock < quantityDifference)
            {
                throw new InvalidOperationException($"Not enough stock for item {item.Name}. Available: {item.Stock}");
            }

            await _itemService.ChangeItemStock(new ChangeItemStockRequest
            {
                BusinessId = request.BusinessId,
                ItemId = itemId,
                StockChange = -quantityDifference
            });
        }

        if (quantity > 0)
        {
            if (oldOrderItem != null)
            {
                oldOrderItem.Quantity = quantity;
                newOrderItems.Add(oldOrderItem);
            }
            else
            {
                newOrderItems.Add(new OrderItem
                {
                    BusinessId = request.BusinessId,
                    ItemId = itemId,
                    Quantity = quantity
                });
            }
        }
    }

    existingOrder.OrderItems = newOrderItems;
    await _orderRepository.Update(existingOrder);
    await _unitOfWork.SaveChanges();

    return true;
}

    public async Task CancelOrder(CancelOrderRequest request)
    {
        var order = await _orderRepository.Get(request.Id);

        if (order == null || order.BusinessId != request.BusinessId || order.Status != OrderStatus.Open)
        {
            return;
        }

        foreach (var orderItem in order.OrderItems)
        {
            await _itemService.ChangeItemStock(new ChangeItemStockRequest
            {
                ItemId = orderItem.Item.Id,
                BusinessId = request.BusinessId,
                StockChange = orderItem.Quantity
            });

            foreach (var itemVariation in orderItem.ItemVariations)
            {
                await _itemService.ChangeItemVariationStock(new ChangeItemVariationStockRequest
                {
                    ItemVariationId = itemVariation.Id,
                    BusinessId = request.BusinessId,
                    StockChange = orderItem.Quantity
                });
            }
        }

        if (order.Reservation != null)
        {
            await _reservationService.CancelReservation(new CancelReservationRequest
            {
                ReservationId = order.Reservation.Id,
                BusinessId = request.BusinessId
            });
        }

        order.Status = OrderStatus.Canceled;

        await _unitOfWork.SaveChanges();
    }

    public async Task AddTip(AddTipRequest request)
    {
        var order = await _orderRepository.Get(request.OrderId);

        if (order == null || order.BusinessId != request.BusinessId || order.Status != OrderStatus.Open)
        {
            return;
        }

        order.TipAmount = request.TipAmount;

        await _unitOfWork.SaveChanges();
    }
}
