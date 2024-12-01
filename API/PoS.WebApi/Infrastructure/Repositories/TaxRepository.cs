using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class TaxRepository : ITaxRepository
{
    private readonly DatabaseContext _dbContext;

    public TaxRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Tax>> GetAll()
    {
        return await _dbContext.Taxes.ToListAsync();
    }

    public Task Update(Tax entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Tax> Get(Guid id)
    {
        return await _dbContext.Taxes.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task Create(Tax tax)
    {
        await _dbContext.Taxes.AddAsync(tax);
    }

    public async Task<IEnumerable<Tax>> GetTaxesByIds(List<Guid> taxIds)
    {
        return await _dbContext.Taxes
            .Where(t => taxIds.Contains(t.Id))
            .ToListAsync();
    }
}