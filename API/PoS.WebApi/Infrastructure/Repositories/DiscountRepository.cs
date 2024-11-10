using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories
{
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

        public async Task<DiscountWithGroupsDto> GetDto(Guid id)
        {
            var discount = await _dbContext.Discounts
            .Include(d => d.GroupDiscounts)
                .ThenInclude(gd => gd.ItemGroup)
            .Where(d => d.Id == id)
            .Select(d => new DiscountWithGroupsDto
            {
                DiscountId = d.Id,
                DiscountName = d.Name,
                Value = d.Value,
                IsPercentage = d.IsPercentage,
                AmountAvailable = d.AmountAvailable,
                ValidFrom = d.ValidFrom,
                ValidTo = d.ValidTo,
                ItemGroups = d.GroupDiscounts.Select(gd => new ItemGroupDto
                {
                    ItemGroupId = gd.ItemGroupId,
                    ItemGroupName = gd.ItemGroup.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();

             return discount;
        }

        public async Task<IEnumerable<DiscountWithGroupsDto>> GetAll(QueryParameters parameters)
        {
            var discountsQuery = _dbContext.Discounts
                .Include(d => d.GroupDiscounts)
                    .ThenInclude(gd => gd.ItemGroup)
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

            // Project into DTO
            var discountsWithGroups = await discountsQuery
                .Select(d => new DiscountWithGroupsDto
                {
                    DiscountId = d.Id,
                    DiscountName = d.Name,
                    Value = d.Value,
                    IsPercentage = d.IsPercentage,
                    AmountAvailable = d.AmountAvailable,
                    ValidFrom = d.ValidFrom,
                    ValidTo = d.ValidTo,
                    ItemGroups = d.GroupDiscounts.Select(gd => new ItemGroupDto
                    {
                        ItemGroupId = gd.ItemGroupId,
                        ItemGroupName = gd.ItemGroup.Name
                    }).ToList()
                })
                .ToListAsync();

            return discountsWithGroups;
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
            return await _dbContext.Discounts.FindAsync(id);
        }

        public async Task<IEnumerable<Discount>> GetAll()
        {
            return await _dbContext.Discounts.Include(d => d.GroupDiscounts).ToListAsync();
        }
    }
}
