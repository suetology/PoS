using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.GroupDiscount.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class GroupDiscountRepository : IGroupDiscountRepository
{
    private readonly DatabaseContext _dbContext;

    public GroupDiscountRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(GroupDiscount entity)
    {
        await _dbContext.GroupDiscounts.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid discountId, Guid itemGroupId)
    {
        var groupDiscount = await GetAsync(discountId, itemGroupId);
        if (groupDiscount != null)
        {
            _dbContext.GroupDiscounts.Remove(groupDiscount);
        }
    }

    public Task<GroupDiscount> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GroupDiscount>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<GroupDiscountDto>> GetAllDto()
    {
        return await _dbContext.GroupDiscounts
            .Include(gd => gd.Discount)
            .Include(gd => gd.ItemGroup)
            .Select(gd => new GroupDiscountDto
            {
                 DiscountId = gd.DiscountId,
                 DiscountName = gd.Discount.Name,
                 ItemGroupId = gd.ItemGroupId,
                 ItemGroupName = gd.ItemGroup.Name
             })
            .ToListAsync();
    }

    public async Task<GroupDiscount> GetAsync(Guid discountId, Guid itemGroupId)
    {
        return await _dbContext.GroupDiscounts
               .FirstOrDefaultAsync(gd => gd.DiscountId == discountId && gd.ItemGroupId == itemGroupId);
    }

    public async Task<IEnumerable<GroupDiscount>> GetByDiscountIdAsync(Guid discountId)
    {
        return await _dbContext.GroupDiscounts
            .Where(gd => gd.DiscountId == discountId)
            .ToListAsync();
    }

    public async  Task<IEnumerable<GroupDiscount>> GetByItemGroupIdAsync(Guid itemGroupId)
    {
        return await _dbContext.GroupDiscounts
               .Where(gd => gd.ItemGroupId == itemGroupId)
               .ToListAsync();
    }
}
