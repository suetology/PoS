using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Item;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly DatabaseContext _dbContext;

    public ItemRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Item entity)
    {
        await _dbContext.Items.AddAsync(entity);
    }

    public async Task<Item> Get(Guid id)
    {
        return await _dbContext.Items
            .Include(i => i.ItemTaxes)
                .ThenInclude(it => it.Tax)
            .Include(i => i.ItemGroup)
            .Include(i => i.ItemDiscounts)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Item>> GetAll()
    {
        return await _dbContext.Items
            .Include(i => i.ItemTaxes)
                .ThenInclude(it => it.Tax)
            .Include(i => i.ItemGroup)
            .ToListAsync();
    }

    public Task Update(Item entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Item>> GetAllItemsByFiltering(QueryParameters parameters)
    {
        var allItems = _dbContext.Items
            .Include(i => i.ItemTaxes)
                .ThenInclude(it => it.Tax)
            .Include(i => i.ItemGroup)
            .AsQueryable();

        // Filtering by search
        if (!string.IsNullOrEmpty(parameters.Search))
        {
            allItems = allItems.Where(u =>
                u.Name.Contains(parameters.Search) ||
                u.Description.Contains(parameters.Search));
        }

        allItems = allItems
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);

        return await allItems.ToListAsync();
    }
}
