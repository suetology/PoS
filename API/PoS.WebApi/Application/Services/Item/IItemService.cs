﻿namespace PoS.WebApi.Application.Services.Item;

using PoS.WebApi.Application.Services.Item.Contracts;

public interface IItemService
{
    Task<GetAllItemsResponse> GetAllItems(GetAllItemsRequest request);
    Task<GetItemResponse> GetItem(GetItemRequest request);
    Task CreateItem(CreateItemRequest request);
    Task ChangeItemStock(ChangeItemStockRequest request);
    Task<GetAllItemVariationsResponse> GetAllItemVariations(GetAllItemVariationsRequest request);
    Task<GetItemVariationResponse> GetItemVariation(GetItemVariationRequest request);
    Task CreateItemVariation(CreateItemVariationRequest request);
    Task ChangeItemVariationStock(ChangeItemVariationStockRequest request);
    Task<bool> UpdateItem(UpdateItemRequest request);
}
