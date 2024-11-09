
namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Domain.Entities;

public interface IOrderService {
    Task CreateOrder(OrderDto orderDto);
    Task<IEnumerable<Order>> GetAllOrders(OrderQueryParameters parameters);
    Task<Order> GetOrder(Guid id);
}