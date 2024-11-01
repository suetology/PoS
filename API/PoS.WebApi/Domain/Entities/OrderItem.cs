using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class OrderItem : Entity
{
    public string Name { get; set; } // nelabai reikia cj, nes is product Name ateina
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; } // sito siaip neturi buti cia, nes skaiciuojasi is Product
    
    public Guid ItemId { get; set; }
    
    public Item Item { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }
}