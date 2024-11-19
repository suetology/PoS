using PoS.WebApi.Application.Services.GroupDiscount.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IGroupDiscountRepository : IRepository<GroupDiscount>
{
    Task DeleteAsync(Guid discountId, Guid itemGroupId);
    Task<IEnumerable<GroupDiscount>> GetByDiscountIdAsync(Guid discountId);
    Task<IEnumerable<GroupDiscount>> GetByItemGroupIdAsync(Guid itemGroupId);
    Task<GroupDiscount> GetAsync(Guid discountId, Guid itemGroupId);
    Task<IEnumerable<GroupDiscountDto>> GetAllDto();
}
