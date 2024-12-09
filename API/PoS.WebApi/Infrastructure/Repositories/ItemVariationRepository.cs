using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class ItemVariationRepository : IItemVariationRepository
{
    private readonly DatabaseContext _dbContext;

    public ItemVariationRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ItemVariation> Get(Guid id)
    {
        return await _dbContext.ItemVariations
            .Include(i => i.Item)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task Create(ItemVariation entity)
    {
        await _dbContext.ItemVariations.AddAsync(entity);
    }

    public async Task<IEnumerable<ItemVariation>> GetAll()
    {
        return await _dbContext.ItemVariations
            .Include(i => i.Item)
            .ToListAsync();
    }

    public Task Update(ItemVariation entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ItemVariation>> GetAllItemVariationByItemId(Guid itemId)
    {
        return await _dbContext.ItemVariations
            .Where(i => i.ItemId == itemId)
            .Include(i => i.Item)
            .ToListAsync();
    }
}