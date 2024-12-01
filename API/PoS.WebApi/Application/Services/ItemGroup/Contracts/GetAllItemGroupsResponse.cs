namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

public class GetAllItemGroupsResponse
{
    public IEnumerable<ItemGroupDto> ItemGroups { get; set; }
}