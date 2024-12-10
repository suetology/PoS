
using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Services.Reservation;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Application.Services.Customer;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Application.Services.ServiceCharge.Contracts;

public class OrderService: IOrderService
{
    private readonly IReservationService _reservationService;
    private readonly ICustomerService _customerService;
    private readonly IOrderRepository _orderRepository;
    private readonly IItemVariationRepository _itemVariationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IReservationService reservationService, 
        ICustomerService customerService,
        IOrderRepository orderRepository,
        IItemVariationRepository itemVariationRepository, 
        IUnitOfWork unitOfWork) 
    {
        _reservationService = reservationService;
        _customerService = customerService;
        _orderRepository = orderRepository;
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

                foreach (var itemVariationId in o.ItemVariationsIds)
                {
                    var itemVariation = await _itemVariationRepository.Get(itemVariationId);
                    
                    orderItem.ItemVariations.Add(itemVariation);
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
                //FinalAmount = FinalAmount,
                //PaidAmount = PaidAmount,
                TipAmount = o.TipAmount,
                EmployeeId = o.EmployeeId,
                //DiscountId = o.DiscountId,
                ServiceCharge = o.ServiceCharge == null ? null : new ServiceChargeDto
                {
                    Id = o.ServiceCharge.Id,
                    Name = o.ServiceCharge.Name,
                    Description = o.ServiceCharge.Description,
                    Value = o.ServiceCharge.Value,
                    IsPercentage = o.ServiceCharge.IsPercentage
                },
                //ServiceChargeAmount = ServiceChargeAmount,
                //DiscountAmount = DiscountAmount
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
                //FinalAmount = FinalAmount,
                //PaidAmount = PaidAmount,
                TipAmount = order.TipAmount,
                EmployeeId = order.EmployeeId,
                //DiscountId = order.DiscountId,
                //ServiceChargeAmount = ServiceChargeAmount,
                //DiscountAmount = DiscountAmount
                ServiceCharge = order.ServiceCharge == null ? null : new ServiceChargeDto
                {
                    Id = order.ServiceCharge.Id,    
                    Name = order.ServiceCharge.Name,
                    Description = order.ServiceCharge.Description,
                    Value = order.ServiceCharge.Value,
                    IsPercentage = order.ServiceCharge.IsPercentage
                },
            }
        };
    }
}
