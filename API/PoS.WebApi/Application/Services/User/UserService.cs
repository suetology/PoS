using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.User;

using Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.User;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Common;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUser(CreateUserRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            Status = request.Status,
            DateOfEmployment = request.DateOfEmployment,
            BusinessId = request.BusinessId,
            LastUpdated = DateTime.UtcNow
        };

        await _userRepository.Create(user);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllUsersResponse> GetAllUsers(QueryParameters parameters)
    {
        var users = await _userRepository.GetAllUsersByFiltering(parameters);
        var usersDtos = users
            .Select(u => new UserDto
            {
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                Status = u.Status,
                DateOfEmployment = u.DateOfEmployment
            });

        return new GetAllUsersResponse
        {
            Users = usersDtos
        };
    }

    public async Task<GetUserResponse> GetUser(Guid userId)
    {
        var user = await _userRepository.Get(userId);

        return new GetUserResponse
        {
            User = new UserDto
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Status = user.Status,
                DateOfEmployment = user.DateOfEmployment
            }
        };
    }

    public Task<GetAvailableRolesResponse> GetAvailableRoles()
    {
        return Task.FromResult(new GetAvailableRolesResponse
        {
            Roles = Enum.GetNames(typeof(Role))
        });
    }
}
