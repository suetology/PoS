namespace PoS.WebApi.Application.Services.User;

using PoS.WebApi.Application.Services.User.Contracts;

public interface IUserService
{
    Task CreateUser(CreateUserRequest request);
    Task<GetAllUsersResponse> GetAllUsers(GetAllUsersRequest request);
    Task<GetUserResponse> GetUser(GetUserRequest request);
    Task<GetAvailableRolesResponse> GetAvailableRoles();
    Task<bool> UpdateUser(UpdateUserRequest request);
}