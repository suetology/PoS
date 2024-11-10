namespace PoS.WebApi.Application.Services.Item;

using PoS.WebApi.Application.Services.Item.Contracts;
using Domain.Entities;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllItems(QueryParameters parameters);
    Task<Item> GetItem(Guid itemId);
    Task CreateItem(ItemDto itemDto);
}
