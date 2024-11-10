namespace PoS.WebApi.Application.Services.ItemGroup;

using PoS.WebApi.Domain.Common;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using Domain.Entities;

public class ItemGroupService : IItemGroupService
{
    private readonly IItemGroupRepository _itemGroupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ItemGroupService(IItemGroupRepository itemGroupRepository, IUnitOfWork unitOfWork)
    {
        _itemGroupRepository = itemGroupRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ItemGroup>> GetAllItemGroupsAsync(QueryParameters parameters)
    {
        return await _itemGroupRepository.GetAllGroupsByFiltering(parameters);
    }

    public async Task<ItemGroup> GetItemGroupByIdAsync(Guid id)
    {
        return await _itemGroupRepository.Get(id);
    }

    public async Task CreateItemGroup(ItemGroupDto itemGroupDto)
    {
        var itemGroup = itemGroupDto.ToDomain();
        await _itemGroupRepository.Create(itemGroup);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateItemGroup(Guid id, ItemGroupDto itemGroupDto)
    {
        var existingItemGroup = await _itemGroupRepository.Get(id);
        if (existingItemGroup == null)
        {
            throw new KeyNotFoundException("ItemGroup not found.");
        }

        if (!string.IsNullOrEmpty(itemGroupDto.Name))
        {
            existingItemGroup.Name = itemGroupDto.Name;
        }

        if (!string.IsNullOrEmpty(itemGroupDto.Description))
        {
            existingItemGroup.Description = itemGroupDto.Description;
        }

        await _itemGroupRepository.Update(existingItemGroup);
        await _unitOfWork.SaveChanges();
    }
}
