
namespace PoS.WebApi.Application.Repositories;

using PoS.WebApi.Application.Services.Order;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetAllFiltered(OrderQueryParameters parameters);
}