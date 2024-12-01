namespace PoS.WebApi.Application.Services.Item.Contracts;

public class GetAllItemsResponse
{
    public IEnumerable<ItemDto> Items { get; set; }
}