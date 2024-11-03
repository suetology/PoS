﻿namespace PoS.WebApi.Application.Services.User.Contracts;

using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Enums;

public class UserDto
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    public EmployeeStatus Status { get; set; }
    public DateTime DateOfEmployment { get; set; }
    public Guid BusinessId { get; set; }

    public User ToDomain()
    {
        return new User()
        {
            Username = Username,
            PasswordHash = PasswordHash,
            Name = Name,
            Surname = Surname,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Role = Role,
            Status = Status,
            DateOfEmployment = DateOfEmployment,
            BusinessId = BusinessId,
            LastUpdated = DateTime.UtcNow
        };
    }
}
