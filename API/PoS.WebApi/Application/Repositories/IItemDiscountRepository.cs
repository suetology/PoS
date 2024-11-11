using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Application.Services.ItemDiscount.Contracts;

namespace PoS.WebApi.Application.Repositories;

public interface IItemDiscountRepository : IRepository<ItemDiscount>
{
    Task DeleteAsync(Guid itemId, Guid discountId);
    Task<ItemDiscount> GetAsync(Guid itemId, Guid discountId);
    Task<IEnumerable<ItemDiscountDto>> GetAllDto();
}
