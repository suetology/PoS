
namespace PoS.WebApi.Application.Services.Order;

using PoS.WebApi.Application.Services.Order.Contracts;

public interface IOrderService 
{
    Task CreateOrder(CreateOrderRequest request);
    Task<GetAllOrdersResponse> GetAllOrders(GetAllOrdersRequest request);
    Task<GetOrderResponse> GetOrder(GetOrderRequest request);
    Task CancelOrder(CancelOrderRequest request);
    Task AddTip(AddTipRequest request);
}