﻿namespace PoS.WebApi.Application.Services.ItemGroup;

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
    public async Task<GetAllItemGroupsResponse> GetAllItemGroupsAsync(QueryParameters parameters)
    {
        var itemGroups = await _itemGroupRepository.GetAllGroupsByFiltering(parameters);
        var itemGroupDtos = itemGroups
            .Select(i => new ItemGroupDto
            {
                Name = i.Name,
                Description = i.Description
            });

        return new GetAllItemGroupsResponse
        {
            ItemGroups = itemGroupDtos
        };
    }

    public async Task<GetItemGroupResponse> GetItemGroupByIdAsync(Guid id)
    {
        var itemGroup = await _itemGroupRepository.Get(id);

        return new GetItemGroupResponse
        {
            ItemGroup = new ItemGroupDto
            {
                Name = itemGroup.Name,
                Description = itemGroup.Description
            }
        };
    }

    public async Task CreateItemGroup(CreateItemGroupRequest request)
    {
        var itemGroup = new ItemGroup
        {
            Name = request.Name,
            Description = request.Description
        };
        
        await _itemGroupRepository.Create(itemGroup);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateItemGroup(Guid id, UpdateItemGroupRequest request)
    {
        var existingItemGroup = await _itemGroupRepository.Get(id);
        if (existingItemGroup == null)
        {
            throw new KeyNotFoundException("ItemGroup not found.");
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
