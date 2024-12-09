using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemVariationRepository _itemVariationRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(
            IItemRepository itemRepository,
            IItemVariationRepository itemVariationRepository,
            ITaxRepository taxRepository,
            IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _itemVariationRepository = itemVariationRepository;
            _taxRepository = taxRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateItem(CreateItemRequest request)
        {
            var item = new Domain.Entities.Item
            {
                BusinessId = request.BusinessId,
                Name = request.Name,
                Description = request.Description,
                Image = new byte[0],
                Price = request.Price,
                Stock = request.Stock,
                ItemGroupId = request.ItemGroupId
            };

            var taxes = await _taxRepository.GetTaxesByIds(request.TaxIds);
            
            foreach (var tax in taxes)
            {
                item.Taxes.Add(tax);
            }

            await _itemRepository.Create(item);
            await _unitOfWork.SaveChanges();
        }

        public async Task<GetAllItemsResponse> GetAllItems(GetAllItemsRequest request)
        {
            var items = await _itemRepository.GetAllItemsByFiltering(request.QueryParameters);
            var itemDtos = items
                .Where(i => i.BusinessId == request.BusinessId)
                .Select(i =>
                {
                    var taxIds = i.Taxes.Select(t => t.Id).ToList();
                    
                    return new ItemDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Description = i.Description,
                        Price = i.Price,
                        Stock = i.Stock,
                        Image = i.Image,
                        ItemGroupId = i.ItemGroupId,
                        TaxIds = taxIds
                    };
                });

            return new GetAllItemsResponse
            {
                Items = itemDtos
            };
        }

        public async Task<GetItemResponse> GetItem(GetItemRequest request)
        {
            var item = await _itemRepository.Get(request.ItemId);
            var taxIds = item.Taxes.Select(t => t.Id).ToList();

            if (item?.BusinessId != request.BusinessId)
            {
                return null;
            }
            
            return new GetItemResponse
            {
                Item = new ItemDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Stock = item.Stock,
                    Image = item.Image,
                    ItemGroupId = item.ItemGroupId,
                    TaxIds = taxIds,
                }
            };
        }
        
        public async Task<GetAllItemVariationsResponse> GetAllItemVariations(GetAllItemVariationsRequest request)
        {
            var itemVariations = await _itemVariationRepository.GetAllItemVariationByItemId(request.ItemId);
            var itemVariationsDtos = itemVariations
                .Where(i => i.BusinessId == request.BusinessId)
                .Select(i => new ItemVariationDto
                {   
                    Id = i.Id,
                    ItemId = i.ItemId,
                    Name = i.Name,
                    Description = i.Description,
                    AddedPrice = i.AddedPrice,
                    Stock = i.Stock
                });

            return new GetAllItemVariationsResponse
            {
                ItemVariations = itemVariationsDtos
            };
        }

        public async Task<GetItemVariationResponse> GetItemVariation(GetItemVariationRequest request)
        {
            var itemVariation = await _itemVariationRepository.Get(request.ItemId);

            if (itemVariation?.BusinessId != request.BusinessId)
            {
                return null;
            }

            return new GetItemVariationResponse
            {
                ItemVariation = new ItemVariationDto
                {
                    Id = itemVariation.Id,
                    ItemId = itemVariation.ItemId,
                    Name = itemVariation.Name,
                    Description = itemVariation.Description,
                    AddedPrice = itemVariation.AddedPrice,
                    Stock = itemVariation.Stock
                }
            };
        }

        public async Task CreateItemVariation(CreateItemVariationRequest request)
        {
            var itemVariation = new ItemVariation
            {
                ItemId = request.ItemId,
                BusinessId = request.BusinessId,
                Name = request.Name,
                Description = request.Description,
                AddedPrice = request.AddedPrice,
                Stock = request.Stock,
            };
            
            await _itemVariationRepository.Create(itemVariation);
            await _unitOfWork.SaveChanges();
        }
    }
}
