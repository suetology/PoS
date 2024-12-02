namespace PoS.WebApi.Application.Services.Discount.Contracts;

public class GetAllDiscountsResponse
{
    public IEnumerable<DiscountDto> Discounts { get; set; }
}