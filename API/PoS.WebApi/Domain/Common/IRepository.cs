namespace PoS.WebApi.Domain.Common;

public interface IRepository<T> 
    where T : Entity
{
    Task<T> Get(Guid id);
    
    Task Create(T entity);
    Task<IEnumerable<T>> GetAll();

}