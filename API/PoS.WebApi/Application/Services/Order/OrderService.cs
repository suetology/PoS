
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

    public async Task CreateOrder(OrderDto orderDto)
    {
        var order = orderDto.ToDomain();
        await _orderRepository.Create(order);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Order>> GetAllOrders(OrderQueryParameters parameters)
    {
        return await _orderRepository.GetAllFiltered(parameters);
    }

    public async Task<Order> GetOrder(Guid id)
    {
        return await _orderRepository.Get(id);
    }
}
