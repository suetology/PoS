namespace PoS.WebApi.Application.Services.Discount;

using PoS.WebApi.Application.Services.Discount.Contracts;

public interface IDiscountService
{
    Task CreateDiscount(CreateDiscountRequest request);
    Task<GetAllDiscountsResponse> GetAllDiscounts(GetAllDiscountsRequest request);
    Task<GetDiscountResponse> GetDiscount(GetDiscountRequest request);
    Task DeleteDiscountById(DeleteDiscountRequest request);
}