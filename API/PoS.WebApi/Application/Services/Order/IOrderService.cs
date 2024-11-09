
namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Domain.Entities;

public interface IOrderService {
    Task CreateOrder(OrderDto order);
    Task<IEnumerable<Order>> GetAllOrders(OrderQueryParameters parameters);
    Task<Order> GetOrder(Guid id);
}