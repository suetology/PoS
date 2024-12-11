using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly DatabaseContext _dbContext;
    public DiscountRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(Discount discount)
    {
        await _dbContext.Discounts.AddAsync(discount);
    }

    public async Task<IEnumerable<Discount>> GetAll()
    {
        return await _dbContext.Discounts.ToListAsync();
    }
    
    public async Task<IEnumerable<Discount>> GetAll(QueryParameters parameters)
    {
        var discountsQuery = _dbContext.Discounts
            .Include(d => d.Items)
            .Include(d => d.ItemGroups)
            .AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(parameters.Name))
        {
            discountsQuery = discountsQuery.Where(d => d.Name.Contains(parameters.Name));
        }

        if (parameters.Value.HasValue)
        {
            discountsQuery = discountsQuery.Where(d => d.Value == parameters.Value.Value);
        }

        if (parameters.ValidFrom.HasValue)
        {
            discountsQuery = discountsQuery.Where(d => d.ValidFrom >= parameters.ValidFrom.Value);
        }

        if (parameters.ValidTo.HasValue)
        {
            discountsQuery = discountsQuery.Where(d => d.ValidTo <= parameters.ValidTo.Value);
        }

        // Apply pagination
        discountsQuery = discountsQuery
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
        
        return discountsQuery;
    }

    public async Task Delete(Guid discountId)
    {
        var discount = await _dbContext.Discounts.FindAsync(discountId);
        if (discount != null)
        {
            _dbContext.Discounts.Remove(discount);
        }
    }

    public async Task<Discount> Get(Guid id)
    {
        var discounts = await _dbContext.Discounts
            .Include(d => d.Items)
            .Include(d => d.ItemGroups)
            .ToListAsync();
        
        return discounts.Find(d => d.Id == id);
    }

    public Task Update(Discount entity)
    {
        throw new NotImplementedException();
    }
}
