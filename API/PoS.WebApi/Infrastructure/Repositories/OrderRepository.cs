using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DatabaseContext _dbContext;

    public OrderRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public Task<Order> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAll()
    {
        throw new NotImplementedException();
    }
}
