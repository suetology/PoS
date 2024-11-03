using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<IEnumerable<User>> GetAllUsersByFiltering(QueryParameters parameters);
}
