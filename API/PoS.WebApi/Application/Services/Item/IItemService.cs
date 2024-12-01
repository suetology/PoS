namespace PoS.WebApi.Application.Services.Item;

using PoS.WebApi.Application.Services.Item.Contracts;
using Domain.Entities;

public interface IItemService
{
    Task<GetAllItemsResponse> GetAllItems(QueryParameters parameters);
    Task<GetItemResponse> GetItem(Guid itemId);
    Task CreateItem(CreateItemRequest request);
}
