using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Business : Entity
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    public ICollection<User> Employees { get; set; }

    public void Update(Business entity)
    {
        Name = entity.Name;
        Address = entity.Address;
        PhoneNumber = entity.PhoneNumber;
        Email = entity.Email;
        Employees = entity.Employees;
    }
}