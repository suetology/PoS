﻿using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.User;

using Domain.Entities;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.User.Contracts;
using PoS.WebApi.Domain.Common;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private readonly IShiftRepository _shiftRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IShiftRepository shiftRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _shiftRepository = shiftRepository;
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
            return null;
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

    public Task<GetAvailableRolesResponse> GetAvailableRoles()
    {
        return Task.FromResult(new GetAvailableRolesResponse
        {
            Roles = Enum.GetNames(typeof(Role))
        });
    }
}
