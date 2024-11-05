using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsername(string username);
}
