using PoS.WebApi.Application.Services.ItemDiscount.Contracts;

namespace PoS.WebApi.Application.Services.ItemDiscount;

public interface IItemDiscountService
{
    Task AssignDiscountToItemAsync(Guid discountId, Guid itemId);
    Task RemoveDiscountFromItemAsync(Guid discountId, Guid itemId);
    Task<IEnumerable<ItemDiscountDto>> GetAllItemDiscounts();
}
