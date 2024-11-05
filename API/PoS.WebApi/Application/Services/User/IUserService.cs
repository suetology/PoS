namespace PoS.WebApi.Application.Services.User;

using PoS.WebApi.Application.Services.User.Contracts;
using Domain.Entities;

public interface IUserService
{
    Task CreateUser(UserDto user);
    Task<IEnumerable<User>> GetAllUsers(QueryParameters parameters);
    Task<User> GetUser(Guid id);
}