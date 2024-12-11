using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class OrderItem : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; } // nelabai reikia cj, nes is product Name ateina
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; } // sito siaip neturi buti cia, nes skaiciuojasi is Product
    
    public Guid ItemId { get; set; }
    
    public Item Item { get; set; }

    public ICollection<ItemVariation> ItemVariations { get; set; } = new List<ItemVariation>();
    
    public Guid OrderId { get; set; }
    
    public Order Order { get; set; }

    public decimal CalculateTotalAmout()
    {
        return Quantity * (Item.CalculateTotalAmount() + ItemVariations.Sum(v => v.AddedPrice));
    }
}