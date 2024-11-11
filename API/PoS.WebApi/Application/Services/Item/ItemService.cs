using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITaxRepository _taxRepository;

        public ItemService(
            IItemRepository itemRepository,
            IUnitOfWork unitOfWork, ITaxRepository taxRepository)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
            _taxRepository = taxRepository;
        }

        public async Task CreateItem(ItemDto itemDto)
        {
            var item = itemDto.ToDomain();

            var taxes = await _taxRepository.GetTaxesByIds(itemDto.TaxIds);

            // Create ItemTax relationships
            foreach (var tax in taxes)
            {
                var itemTax = new ItemTax
                {
                    Item = item,
                    Tax = tax
                };
                item.ItemTaxes.Add(itemTax);
            }

            await _itemRepository.Create(item);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<Domain.Entities.Item>> GetAllItems(QueryParameters parameters)
        {
            return await _itemRepository.GetAllItemsByFiltering(parameters);
        }

        public async Task<Domain.Entities.Item> GetItem(Guid itemId)
        {
            return await _itemRepository.Get(itemId);
        }
    }
}
