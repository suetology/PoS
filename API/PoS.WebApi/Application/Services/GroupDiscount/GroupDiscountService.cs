namespace PoS.WebApi.Application.Services.GroupDiscount;

using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.GroupDiscount.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GroupDiscountService : IGroupDiscountService
{
    private readonly IGroupDiscountRepository _groupDiscountRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IItemGroupRepository _itemGroupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GroupDiscountService(
        IGroupDiscountRepository groupDiscountRepository,
        IDiscountRepository discountRepository,
        IItemGroupRepository itemGroupRepository,
        IUnitOfWork unitOfWork)
    {
        _groupDiscountRepository = groupDiscountRepository;
        _discountRepository = discountRepository;
        _itemGroupRepository = itemGroupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AssignDiscountToGroupAsync(Guid discountId, Guid itemGroupId)
    {
        var discount = await _discountRepository.Get(discountId);
        if (discount == null)
            throw new KeyNotFoundException("Discount not found.");

        var itemGroup = await _itemGroupRepository.Get(itemGroupId);
        if (itemGroup == null)
            throw new KeyNotFoundException("ItemGroup not found.");

        var existingGroupDiscount = await _groupDiscountRepository.GetAsync(discountId, itemGroupId);
        if (existingGroupDiscount != null)
            throw new InvalidOperationException("Discount is already assigned to this ItemGroup.");

        // Create association
        var groupDiscount = new GroupDiscount
        {
            DiscountId = discountId,
            ItemGroupId = itemGroupId
        };

        await _groupDiscountRepository.Create(groupDiscount);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<GroupDiscount>> GetDiscountsByGroupAsync(Guid itemGroupId)
    {
        return await _groupDiscountRepository.GetByItemGroupIdAsync(itemGroupId);
    }

    public async Task<IEnumerable<GroupDiscount>> GetGroupsByDiscountAsync(Guid discountId)
    {
        return await _groupDiscountRepository.GetByDiscountIdAsync(discountId);
    }

    public async Task RemoveDiscountFromGroupAsync(Guid discountId, Guid itemGroupId)
    {
        await _groupDiscountRepository.DeleteAsync(discountId, itemGroupId);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<GroupDiscountDto>> GetAllGroups()
    {
        return await _groupDiscountRepository.GetAllDto();
    }

}
