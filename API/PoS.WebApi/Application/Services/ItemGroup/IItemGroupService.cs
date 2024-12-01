namespace PoS.WebApi.Application.Services.ItemGroup;

using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using Domain.Entities;

public interface IItemGroupService
{
    Task<GetAllItemGroupsResponse> GetAllItemGroupsAsync(QueryParameters parameters);
    Task<GetItemGroupResponse> GetItemGroupByIdAsync(Guid id);
    Task CreateItemGroup(CreateItemGroupRequest request);
    Task UpdateItemGroup(Guid id, UpdateItemGroupRequest request);
}
