namespace PoS.WebApi.Application.Services.Discount;

using PoS.WebApi.Application.Services.Discount.Contracts;

public interface IDiscountService
{
    Task CreateDiscount(DiscountDto discount);
    Task<IEnumerable<DiscountWithGroupsDto>> GetAllDiscounts(QueryParameters parameters);
    Task<DiscountWithGroupsDto> GetDiscount(Guid id);
    Task DeleteDiscountById(Guid id);
}