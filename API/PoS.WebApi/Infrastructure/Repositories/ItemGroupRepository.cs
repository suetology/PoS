using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.ItemGroup;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class ItemGroupRepository : IItemGroupRepository
{
    private readonly DatabaseContext _dbContext;

    public ItemGroupRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(ItemGroup entity)
    {
        await _dbContext.ItemGroups.AddAsync(entity);
    }

    public async Task<ItemGroup> Get(Guid id)
    {
        return await _dbContext.ItemGroups.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<ItemGroup>> GetAll()
    {
        return await _dbContext.ItemGroups.ToListAsync();
    }

    public async Task<IEnumerable<ItemGroup>> GetAllGroupsByFiltering(QueryParameters parameters)
    {
        var allGroups = _dbContext.ItemGroups.AsQueryable();

        //Filtering by search
        if (!string.IsNullOrEmpty(parameters.Search))
        {
            allGroups = allGroups.Where(u =>
                u.Name.Contains(parameters.Search));
        }

        var pagedGroups = await allGroups
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return pagedGroups;
    }
    public async Task Update(ItemGroup entity)
    {
        _dbContext.ItemGroups.Update(entity);
    }
}
