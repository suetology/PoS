using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class OrderService : Entity
{
    public decimal Price { get; set; }  // ar tikrai reik cia Price kai Service jau turi
        
    public Guid ServiceId { get; set; }
    
    public Service Service { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}