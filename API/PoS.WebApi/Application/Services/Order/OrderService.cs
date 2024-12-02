
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Domain.Common;

public class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateOrder(CreateOrderRequest request)
    {
        var order = new Order
        {   
            BusinessId = request.BusinessId,
            EmployeeId = request.EmployeeId,
            DiscountId = request.DiscountId,
            ServiceChargeId = request.ServiceChargeId,
            Status = OrderStatus.Open,
            Created = DateTime.UtcNow,
            // dar reiks pagalvot cia
            OrderItems = (ICollection<OrderItem>)request.OrderItems
                .Select(o => new OrderItem
                {
                    ItemId = o.ItemId,
                    Quantity = o.Quantity
                })
        };
        
        await _orderRepository.Create(order);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllOrdersResponse> GetAllOrders(GetAllOrdersRequest request)
    {
        var orders = await _orderRepository.GetAllFiltered(request.QueryParameters);
        var orderDtos = orders
            .Where(o => o.BusinessId == request.BusinessId)
            .Select(o => new OrderDto
            {
                Status = o.Status,
                Created = o.Created,
                Closed = o.Closed,
                //FinalAmount = FinalAmount,
                //PaidAmount = PaidAmount,
                TipAmount = o.TipAmount,
                EmployeeId = o.EmployeeId,
                ServiceChargeId = o.ServiceChargeId,
                //ServiceChargeAmount = ServiceChargeAmount,
                DiscountId = o.DiscountId,
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
                Status = order.Status,
                Created = order.Created,
                Closed = order.Closed,
                //FinalAmount = FinalAmount,
                //PaidAmount = PaidAmount,
                TipAmount = order.TipAmount,
                EmployeeId = order.EmployeeId,
                ServiceChargeId = order.ServiceChargeId,
                //ServiceChargeAmount = ServiceChargeAmount,
                DiscountId = order.DiscountId,
                //DiscountAmount = DiscountAmount
            }
        };
    }
}
