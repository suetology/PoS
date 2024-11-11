namespace PoS.WebApi.Application.Services.ItemDiscount;

using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.ItemDiscount.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

public class ItemDiscountService : IItemDiscountService
{
    private readonly IItemDiscountRepository _itemDiscountRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ItemDiscountService(
        IItemDiscountRepository itemDiscountRepository,
        IItemRepository itemRepository,
        IDiscountRepository discountRepository,
        IUnitOfWork unitOfWork)
    {
        _itemDiscountRepository = itemDiscountRepository;
        _itemRepository = itemRepository;
        _discountRepository = discountRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AssignDiscountToItemAsync(Guid discountId, Guid itemId)
    {
        var discount = await _discountRepository.Get(discountId);
        if (discount == null)
            throw new KeyNotFoundException("Discount not found.");

        var item = await _itemRepository.Get(itemId);
        if (item == null)
            throw new KeyNotFoundException("Item not found.");

        var existingItemDiscount = await _itemDiscountRepository.GetAsync(itemId, discountId);
        if (existingItemDiscount != null)
            throw new InvalidOperationException("Discount is already assigned to this item.");

        // Create association
        var itemDiscount = new ItemDiscount
        {
            DiscountId = discountId,
            ItemId = itemId
        };

        await _itemDiscountRepository.Create(itemDiscount);
        await _unitOfWork.SaveChanges();
    }

    public async Task RemoveDiscountFromItemAsync(Guid discountId, Guid itemId)
    {
        await _itemDiscountRepository.DeleteAsync(itemId, discountId);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<ItemDiscountDto>> GetAllItemDiscounts()
    {
        return await _itemDiscountRepository.GetAllDto();
    }
}
