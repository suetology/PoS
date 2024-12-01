
namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Domain.Entities;

public interface IOrderService {
    Task CreateOrder(CreateOrderRequest request);
    Task<GetAllOrdersResponse> GetAllOrders(OrderQueryParameters parameters);
    Task<GetOrderResponse> GetOrder(Guid id);
}