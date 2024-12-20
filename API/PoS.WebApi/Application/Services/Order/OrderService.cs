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
using PoS.WebApi.Application.Services.Notification;
using Amazon.SimpleNotificationService.Model;
using PoS.WebApi.Application.Services.Payments;
using PoS.WebApi.Application.Services.Payments.Contracts;
using PoS.WebApi.Application.Services.Refund.Contracts;
using PoS.WebApi.Application.Services.Order.Exceptions;

public class OrderService: IOrderService
{
    private readonly IReservationService _reservationService;
    private readonly ICustomerService _customerService;
    private readonly INotificationService _notificationService;
    private readonly IItemService _itemService;
    private readonly IPaymentService _paymentService;
    private readonly IOrderRepository _orderRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IItemVariationRepository _itemVariationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IReservationService reservationService, 
        ICustomerService customerService,
        INotificationService notificationService,
        IItemService itemService,
        IPaymentService paymentService,
        IOrderRepository orderRepository,
        IItemRepository itemRepository,
        IItemVariationRepository itemVariationRepository, 
        IUnitOfWork unitOfWork) 
    {
        _reservationService = reservationService;
        _customerService = customerService;
        _notificationService = notificationService;
        _itemService = itemService;
        _paymentService = paymentService;
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

        var customer = await _customerService.GetCustomer(new GetCustomerRequest{
            Id = (Guid)request.CustomerId,
            BusinessId = request.BusinessId,
        });

        if (customer == null) 
        {
            throw new KeyNotFoundException("Unable to find a customer");
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

        using (var transaction = await _unitOfWork.BeginTransaction())
        {
            try
            {
                await _orderRepository.Create(order);
                await _unitOfWork.SaveChanges();
                
                if (request.Reservation != null)
                {
                    request.Reservation.BusinessId = order.BusinessId;
                    request.Reservation.OrderId = order.Id;

                    await _reservationService.CreateReservation(request.Reservation);
                    
                    string message = $"Your reservation has been scheduled for {request.Reservation.AppointmentTime}.";
                    // if(message.Length < 100 && customer.Customer.PhoneNumber.Length < 16) { // Just in case we fuck something up
                    //     Task.Run(() => _notificationService.SendSMS(message, customer.Customer.PhoneNumber));
                    // }
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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
                        ItemGroup = i.Item.ItemGroup == null ? null : new ItemGroupDto
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
                },
                Refund = o.Refund == null ? null : new RefundDto
                {
                    Id = o.Refund.Id,
                    Amount = o.Refund.Amount,
                    Date = o.Refund.Date,
                    Reason = o.Refund.Reason
                }
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
            throw new KeyNotFoundException("Order is not found");
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
                        ItemGroup = i.Item.ItemGroup == null ? null : new ItemGroupDto
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
                },
                Refund = order.Refund == null ? null : new RefundDto
                {
                    Id = order.Refund.Id,
                    Amount = order.Refund.Amount,
                    Date = order.Refund.Date,
                    Reason = order.Refund.Reason
                }
            }
        };
    }

    public async Task<bool> UpdateItemQuantityInOrder(UpdateOrderRequest request)
    {
        var existingOrder = await _orderRepository.Get(request.Id);

        if (existingOrder == null || existingOrder.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Order not found");
        } 
        
        if (OrderStatus.Open != existingOrder.Status)
        {
            throw new InvalidOrderStateException("Only open orders can be modified");
        }

        foreach (var requestOrderItem in request.OrderItems)
        {
            var existingOrderItem = existingOrder.OrderItems
                .FirstOrDefault(oi => oi.ItemId == requestOrderItem.ItemId);

            if (existingOrderItem != null)
            {
                int quantityDifference = requestOrderItem.Quantity - existingOrderItem.Quantity;

                if (quantityDifference != 0)
                {
                    // update quantity if the exact same item already exist
                    var item = await _itemRepository.Get(requestOrderItem.ItemId);

                    if (quantityDifference > 0 && item.Stock < quantityDifference)
                        throw new InvalidOperationException($"Not enough stock for item {item.Name}. Available: {item.Stock}");

                    await _itemService.ChangeItemStock(new ChangeItemStockRequest
                    {
                        BusinessId = request.BusinessId,
                        ItemId = requestOrderItem.ItemId,
                        StockChange = -quantityDifference
                    });

                    existingOrderItem.Quantity = requestOrderItem.Quantity;
                }
            }
            else
            {
                //add as a new item
                var item = await _itemRepository.Get(requestOrderItem.ItemId);
                if (item.Stock < requestOrderItem.Quantity)
                    throw new InvalidOperationException($"Not enough stock for item {item.Name}.");

                var newOrderItem = new OrderItem
                {
                    BusinessId = request.BusinessId,
                    ItemId = requestOrderItem.ItemId,
                    Quantity = requestOrderItem.Quantity
                };

                existingOrder.OrderItems.Add(newOrderItem);
                await _itemService.ChangeItemStock(new ChangeItemStockRequest
                {
                    BusinessId = request.BusinessId,
                    ItemId = requestOrderItem.ItemId,
                    StockChange = -requestOrderItem.Quantity
                });
            }
        }

        await _orderRepository.Update(existingOrder);
        await _unitOfWork.SaveChanges();

        return true;
    }

    public async Task<bool> AddItemToOrder(AddItemToUpdateOrderRequest request)
    {
        var order = await _orderRepository.Get(request.Id);

        if (order == null || order.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Order not found");
        } 
        
        if (OrderStatus.Open != order.Status)
        {
            throw new InvalidOrderStateException("Only open orders can be modified");
        }

        var item = await _itemRepository.Get(request.ItemId);

        if (item.Stock < request.Quantity)
            throw new InvalidOperationException($"Not enough stock for item {item.Name}.");

        await _itemService.ChangeItemStock(new ChangeItemStockRequest
        {
            BusinessId = request.BusinessId,
            ItemId = request.ItemId,
            StockChange = -request.Quantity
        });

        var orderItem = new OrderItem
        {
            BusinessId = request.BusinessId,
            ItemId = request.ItemId,
            Quantity = request.Quantity,
            OrderId = order.Id
        };

        foreach (var itemVariationId in request.ItemVariationsIds)
        {
            var itemVariation = await _itemVariationRepository.Get(itemVariationId);

            orderItem.ItemVariations.Add(itemVariation);

            await _itemService.ChangeItemVariationStock(new ChangeItemVariationStockRequest
            {
                ItemVariationId = itemVariation.Id,
                BusinessId = request.BusinessId,
                StockChange = -request.Quantity
            });
        }

        order.OrderItems.Add(orderItem);
        await _orderRepository.Update(order);
        await _unitOfWork.SaveChanges();

        return true;
    }

    public async Task<bool> UpdateReservation(UpdateOrderReservationRequest request) {
        
        var existingOrder = await _orderRepository.Get(request.Id);

        if (existingOrder == null || existingOrder.BusinessId != request.Reservation.BusinessId)
        {
            throw new KeyNotFoundException("Order not found");
        } 
        
        if (OrderStatus.Open != existingOrder.Status)
        {
            throw new InvalidOrderStateException("Only open orders can be modified");
        }

        await _reservationService.UpdateReservation(request.Reservation);
        await _unitOfWork.SaveChanges();

        return true;
    }

    public async Task CancelOrder(CancelOrderRequest request)
    {
        var order = await _orderRepository.Get(request.Id);

        if (order == null || order.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Order not found");
        } 
        
        if (order.Status != OrderStatus.Open)
        {
            throw new InvalidOrderStateException("Only open orders can be canceled");
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

    public async Task RetireOpenOrders(RetireOpenOrdersRequest request)
    {
        var orders = await _orderRepository.GetAll();
        var orderIds = orders
            .Where(o => o.BusinessId == request.BusinessId && o.CustomerId == request.CustomerId && OrderStatus.Open == o.Status)
            .Select(o => o.Id)
            .ToArray();

        foreach (var Id in orderIds)
        {
            var cancelOrderRequest = new CancelOrderRequest
            {
                Id = Id,
                BusinessId = request.BusinessId
            };

            await CancelOrder(cancelOrderRequest);
        }
    }

    public async Task RetireOrdersWithReservation(RetireOrdersWithReservationRequest request)
    {
        var orders = await _orderRepository.GetAll();
        var orderIds = orders
            .Where(o => o.BusinessId == request.BusinessId && null != o.Reservation && o.Reservation.ServiceId == request.ServiceId && OrderStatus.Open == o.Status)
            .Select(o => o.Id)
            .ToArray();

        foreach (var id in orderIds)
        {
            var cancelOrderRequest = new CancelOrderRequest
            {
                Id = id,
                BusinessId = request.BusinessId
            };

            await CancelOrder(cancelOrderRequest);
        }
    }

    public async Task AddTip(AddTipRequest request)
    {
        var order = await _orderRepository.Get(request.OrderId);

        if (order == null || order.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Order not found");
        } 
        
        if (OrderStatus.Open != order.Status)
        {
            throw new InvalidOrderStateException("Only open orders can be modified");
        }

        order.TipAmount = request.TipAmount;

        await _unitOfWork.SaveChanges();
    }
    
    public async Task RemoveItemFromOrder(RemoveItemFromOrderRequest request)
    {
        var order = await _orderRepository.Get(request.OrderId);

        if (order == null || order.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Order not found");
        } 
        
        if (OrderStatus.Open != order.Status)
        {
            throw new InvalidOrderStateException("Only open orders can be modified");
        }

        var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == request.ItemId);


        if (orderItem == null)
        {
            throw new KeyNotFoundException("Item not found in the order.");
        }

        await _itemService.ChangeItemStock(new ChangeItemStockRequest
        {
            ItemId = request.ItemId,
            BusinessId = request.BusinessId,
            StockChange = orderItem.Quantity
        });

        order.OrderItems.Remove(orderItem);

        await _orderRepository.Update(order);
        await _unitOfWork.SaveChanges();
    }
}
