using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Item : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public byte[] Image { get; set; }
    
    public decimal Price { get; set; } // doke buvo parasyta kad int yra, bet manau klaida
    
    public int Stock { get; set; }
    
    public Guid? ItemGroupId { get; set; }
    
    public ItemGroup? ItemGroup { get; set; }
    
    public ICollection<Tax> Taxes { get; set; } = new List<Tax>();

    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}