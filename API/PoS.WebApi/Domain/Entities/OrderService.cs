using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class OrderService : Entity
{
    public decimal Price { get; set; }  // ar tikrai reik cia Price kai Service jau turi
    //Yes, the OrderService (or OrderItem) should have its own Price property, even though the Service entity already has one.
    //This is crucial for maintaining historical accuracy and data integrity, especially when prices of services or items can change over time.

    public Guid ServiceId { get; set; }
    
    public Service Service { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}