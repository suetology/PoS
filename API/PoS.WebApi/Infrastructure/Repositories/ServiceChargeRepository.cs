using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

public class ServiceChargeRepository : IServiceChargeRepository
{
    private readonly DatabaseContext _dbContext;

    public ServiceChargeRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ServiceCharge>> GetAllServiceCharges()
    {
        return await _dbContext.ServiceCharges.ToListAsync();
    }

    public async Task<ServiceCharge> Get(Guid id)
    {
        return await _dbContext.ServiceCharges.FindAsync(id);
    }

    public async Task Create(ServiceCharge serviceCharge)
    {
        await _dbContext.ServiceCharges.AddAsync(serviceCharge);
    }
}
