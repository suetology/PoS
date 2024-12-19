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
    public async Task<GetAllItemGroupsResponse> GetAllItemGroupsAsync(GetAllItemGroupsRequest request)
    {
        var itemGroups = await _itemGroupRepository.GetAllGroupsByFiltering(request.QueryParameters);
        var itemGroupDtos = itemGroups
            .Where(i => i.BusinessId == request.BusinessId)
            .Select(i => new ItemGroupDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description
            });

        return new GetAllItemGroupsResponse
        {
            ItemGroups = itemGroupDtos
        };
    }

    public async Task<GetItemGroupResponse> GetItemGroupByIdAsync(GetItemGroupRequest request)
    {
        var itemGroup = await _itemGroupRepository.Get(request.Id);

        if (itemGroup.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Item Group is not found");
        }
        
        return new GetItemGroupResponse
        {
            ItemGroup = new ItemGroupDto
            {
                Id = itemGroup.Id,
                Name = itemGroup.Name,
                Description = itemGroup.Description
            }
        };
    }

    public async Task CreateItemGroup(CreateItemGroupRequest request)
    {
        var itemGroup = new ItemGroup
        {
            BusinessId = request.BusinessId,
            Name = request.Name,
            Description = request.Description
        };
        
        await _itemGroupRepository.Create(itemGroup);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateItemGroup(UpdateItemGroupRequest request)
    {
        var existingItemGroup = await _itemGroupRepository.Get(request.Id);
        
        if (existingItemGroup == null || existingItemGroup.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("ItemGroup is not found.");
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            existingItemGroup.Name = request.Name;
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            existingItemGroup.Description = request.Description;
        }

        await _itemGroupRepository.Update(existingItemGroup);
        await _unitOfWork.SaveChanges();
    }
}
