namespace PoS.WebApi.Application.Services.ItemGroup;

using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using Domain.Entities;

public interface IItemGroupService
{
    Task<GetAllItemGroupsResponse> GetAllItemGroupsAsync(GetAllItemGroupsRequest request);
    Task<GetItemGroupResponse> GetItemGroupByIdAsync(GetItemGroupRequest request);
    Task CreateItemGroup(CreateItemGroupRequest request);
    Task UpdateItemGroup(UpdateItemGroupRequest request);
}
