using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Service : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public int Duration { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public Guid EmployeeId { get; set; }
    
    public User Employee { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}