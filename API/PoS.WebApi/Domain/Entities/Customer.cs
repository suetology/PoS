using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Customer : Entity
{
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public ICollection<Reservation> Reservations { get; set; }
}