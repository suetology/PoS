using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class BusinessRepository : IBusinessRepository
{
    private readonly DatabaseContext _dbContext;

    public BusinessRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Business>> GetAll()
    {
        return await _dbContext.Businesses.Include(b => b.Employees).ToListAsync();
    }

    public async Task<Business> Get(Guid id)
    {
        return await _dbContext.Businesses.Include(b => b.Employees).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task Create(Business business)
    {
        await _dbContext.Businesses.AddAsync(business);
    }

    public void Update(Business business)
    {
        _dbContext.Businesses.Update(business);
    }
}
