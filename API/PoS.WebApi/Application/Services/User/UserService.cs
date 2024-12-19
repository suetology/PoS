using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Application.Services.User;

using Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Customer.Contracts;
using PoS.WebApi.Application.Services.Order.Exceptions;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Common;

public class UserService : IUserService
{
    private readonly DatabaseContext _dbContext;
    
    private readonly IUserRepository _userRepository;

    private readonly IShiftRepository _shiftRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        DatabaseContext dbContext,
        IUserRepository userRepository,
        IShiftRepository shiftRepository,
        IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _shiftRepository = shiftRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUser(CreateUserRequest request)
    {
        if (request.Role == Role.BusinessOwner)
        {
            var users = await _userRepository.GetAll();
            var businessUsers = users.Where(u => u.BusinessId == request.BusinessId);

            if (businessUsers.Any(u => u.Role == Role.BusinessOwner))
            {
                throw new InvalidUserRoleException("Business can have only one owner");
            }
        }

        var user = new User
        {
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            Status = UserStatus.Active,
            DateOfEmployment = DateTime.UtcNow,
            BusinessId = request.BusinessId,
            LastUpdated = DateTime.UtcNow
        };

        await _userRepository.Create(user);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllUsersResponse> GetAllUsers(GetAllUsersRequest request)
    {
        var shifts = await _shiftRepository.GetAll();

        var users = await _userRepository.GetAllUsersByFiltering(request.QueryParameters);
        var usersDtos = users
            .Where(u => u.BusinessId == request.BusinessId)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                Status = u.Status,
                DateOfEmployment = u.DateOfEmployment,
                Shifts = shifts
                    .Where(s => s.EmployeeId == u.Id)
                    .Select(s => new ShiftDto
                    {
                        Id = s.Id,
                        Date = s.Date.ToString("yyyy-MM-dd"),
                        StartTime = s.StartTime.ToString("HH:mm"),
                        EndTime = s.EndTime.ToString("HH:mm")
                    })
            });

        return new GetAllUsersResponse
        {
            Users = usersDtos
        };
    }

    public async Task<GetUserResponse> GetUser(GetUserRequest request)
    {
        var user = await _userRepository.Get(request.Id);

        var userShifts = await _shiftRepository.GetShiftsByFilters(employeeId: request.Id);

        if (user == null || user.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("User is not found");
        }

        return new GetUserResponse
        {
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                Status = user.Status,
                DateOfEmployment = user.DateOfEmployment,
                Shifts = userShifts.Select(s => new ShiftDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("yyyy-MM-dd"),
                    StartTime = s.StartTime.ToString("HH:mm"),
                    EndTime = s.EndTime.ToString("HH:mm")
                })
            }
        };
    }

    public async Task<bool> UpdateUser(UpdateUserRequest request)
    {
        var existingUser = await _userRepository.Get(request.Id);

        if (existingUser == null)
        {
            throw new KeyNotFoundException("User is not found");
        }

        existingUser.Username = request.Username ?? existingUser.Username;
        existingUser.PasswordHash = request.PasswordHash ?? existingUser.PasswordHash;
        existingUser.Name = request.Name ?? existingUser.Name;
        existingUser.Surname = request.Surname ?? existingUser.Surname;
        existingUser.Email = request.Email ?? existingUser.Email;
        existingUser.PhoneNumber = request.PhoneNumber ?? existingUser.PhoneNumber;
        existingUser.Role = request.Role;

        if (request.Role == Role.BusinessOwner)
        {
            var users = await _userRepository.GetAll();
            var businessUsers = users.Where(u => u.BusinessId == request.BusinessId);

            if (businessUsers.Any(u => u.Role == Role.BusinessOwner))
            {
                throw new InvalidUserRoleException("Business can have only one owner");
            }
        }

        await _userRepository.Update(existingUser);
        await _unitOfWork.SaveChanges();

        return true;
    }

    public Task<GetAvailableRolesResponse> GetAvailableRoles()
    {
        return Task.FromResult(new GetAvailableRolesResponse
        {
            Roles = Enum.GetNames(typeof(Role))
        });
    }

    public async Task SetBusiness(SetBusinessRequest request)
    {
        var user = await _userRepository.Get(request.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException("User is not found");
        }
        
        user.BusinessId = request.BusinessId;
        
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllUsersResponse> GetAllActiveUsers(GetAllUsersRequest request)
    {
        var shifts = await _shiftRepository.GetAll();

        var users = await _userRepository.GetAllUsersByFiltering(request.QueryParameters);
        var usersDtos = users
            .Where(u => u.BusinessId == request.BusinessId && UserStatus.Active == u.Status)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role,
                Status = u.Status,
                DateOfEmployment = u.DateOfEmployment,
                Shifts = shifts
                    .Where(s => s.EmployeeId == u.Id)
                    .Select(s => new ShiftDto
                    {
                        Id = s.Id,
                        Date = s.Date.ToString("yyyy-MM-dd"),
                        StartTime = s.StartTime.ToString("HH:mm"),
                        EndTime = s.EndTime.ToString("HH:mm")
                    })
            });

        return new GetAllUsersResponse
        {
            Users = usersDtos
        };
    }

    public async Task RetireUser(RetireUserRequest request)
    {
        var existingUser = await _userRepository.Get(request.Id);
        if (existingUser == null || existingUser.BusinessId != request.BusinessId || UserStatus.Left == existingUser.Status)
        {
            throw new KeyNotFoundException("User not found or unauthorised.");
        }
        
        existingUser.Status = UserStatus.Left;
        
        await _userRepository.Update(existingUser);
        await _unitOfWork.SaveChanges();
    }
}
