namespace PoS.WebApi.Domain.Entities;

public class Business : Entity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    public ICollection<User> Employees { get; set; }
}