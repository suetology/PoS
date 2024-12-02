using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Domain.Entities;

public class User : Entity
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
    
    public DateTime LastUpdated { get; set; }
    
    public Guid? BusinessId { get; set; }
    
    public Business Business { get; set; }
    
    public ICollection<Shift> Shifts { get; set; }
    
    public ICollection<Service> Services { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
    
    public ICollection<Order> Orders { get; set; }
}