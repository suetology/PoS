namespace PoS.WebApi.Application.Services.User;

using PoS.WebApi.Application.Services.User.Contracts;
using Domain.Entities;

public interface IUserService
{
    Task CreateUser(CreateUserRequest request);
    Task<GetAllUsersResponse> GetAllUsers(QueryParameters parameters);
    Task<GetUserResponse> GetUser(Guid id);
    Task<GetAvailableRolesResponse> GetAvailableRoles();
}