namespace PoS.WebApi.Application.Services.ItemGroup;

using PoS.WebApi.Application.Services.ItemGroup.Contracts;
using Domain.Entities;

public interface IItemGroupService
{
    Task<IEnumerable<ItemGroup>> GetAllItemGroupsAsync(QueryParameters parameters);
    Task<ItemGroup> GetItemGroupByIdAsync(Guid id);
    Task CreateItemGroup(ItemGroupDto itemGroupDto);
    Task UpdateItemGroup(Guid id, ItemGroupDto request);
}
