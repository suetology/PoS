using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ITaxRepository _taxRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(
            IItemRepository itemRepository,
            ITaxRepository taxRepository,
            IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
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
                .Select(i => new ItemDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Price = i.Price,
                    Stock = i.Stock,
                    Image = i.Image
                });

            return new GetAllItemsResponse
            {
                Items = itemDtos
            };
        }

        public async Task<GetItemResponse> GetItem(GetItemRequest request)
        {
            var item = await _itemRepository.Get(request.ItemId);

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
                    Image = item.Image
                }
            };
        }
    }
}
