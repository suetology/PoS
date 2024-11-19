namespace PoS.WebApi.Application.Repositories;

using PoS.WebApi.Domain.Common;
using Domain.Entities;
using PoS.WebApi.Application.Services.Item;

public interface IItemRepository : IRepository<Item>
{
    //void Update(Item item);
    Task<IEnumerable<Item>> GetAllItemsByFiltering(QueryParameters parameters);
}
