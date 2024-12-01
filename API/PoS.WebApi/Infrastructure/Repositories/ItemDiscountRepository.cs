using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.ItemDiscount.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class ItemDiscountRepository : IItemDiscountRepository
{
    private readonly DatabaseContext _dbContext;

    public ItemDiscountRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(ItemDiscount entity)
    {
        await _dbContext.ItemDiscounts.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid itemId, Guid discountId)
    {
        var itemDiscount = await GetAsync(itemId, discountId);
        if (itemDiscount != null)
        {
            _dbContext.ItemDiscounts.Remove(itemDiscount);
        }
    }

    public async Task<ItemDiscount> GetAsync(Guid itemId, Guid discountId)
    {
        return await _dbContext.ItemDiscounts
            .FirstOrDefaultAsync(id => id.ItemId == itemId && id.DiscountId == discountId);
    }

    public async Task<IEnumerable<ItemDiscountDto>> GetAllDto()
    {
        return await _dbContext.ItemDiscounts
            .Include(id => id.Item)
            .Include(id => id.Discount)
            .Select(id => new ItemDiscountDto
            {
                ItemId = id.ItemId,
                ItemName = id.Item.Name,
                DiscountId = id.DiscountId,
                DiscountName = id.Discount.Name
            })
            .ToListAsync();
    }

    public Task<ItemDiscount> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ItemDiscount>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Update(ItemDiscount entity)
    {
        throw new NotImplementedException();
    }
}