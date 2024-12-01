using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.User.Contracts;

public class CreateUserRequest
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
}