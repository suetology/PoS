using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Item.Contracts;
using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Application.Services.Item
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(
            IItemRepository itemRepository,
            IUnitOfWork unitOfWork)
        {
            _itemRepository = itemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateItem(ItemDto itemDto)
        {
            var item = itemDto.ToDomain();

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
