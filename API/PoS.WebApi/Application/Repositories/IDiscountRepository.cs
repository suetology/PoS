using PoS.WebApi.Application.Services.Discount;
using PoS.WebApi.Application.Services.Discount.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<IEnumerable<DiscountWithGroupsDto>> GetAll(QueryParameters parameters);
        Task<DiscountWithGroupsDto> GetDto(Guid id);
        Task Delete(Guid discountId);
    }
}
