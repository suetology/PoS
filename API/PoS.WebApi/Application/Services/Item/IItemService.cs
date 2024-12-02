namespace PoS.WebApi.Application.Services.Item;

using PoS.WebApi.Application.Services.Item.Contracts;
using Domain.Entities;

public interface IItemService
{
    Task<GetAllItemsResponse> GetAllItems(GetAllItemsRequest request);
    Task<GetItemResponse> GetItem(GetItemRequest request);
    Task CreateItem(CreateItemRequest request);
}
