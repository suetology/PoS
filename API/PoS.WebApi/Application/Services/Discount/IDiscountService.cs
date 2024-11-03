namespace PoS.WebApi.Application.Services.Discount;

using PoS.WebApi.Application.Services.Discount.Contracts;
using Domain.Entities;

public interface IDiscountService
{
    Task CreateDiscount(DiscountDto discount);
    Task<IEnumerable<Discount>> GetAllDiscounts(QueryParameters parameters);
    Task<Discount> GetDiscount(Guid id);
}