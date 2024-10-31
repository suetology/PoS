namespace PoS.WebApi.Domain.Entities;

public class Item : Entity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public byte[] Image { get; set; }
    
    public decimal Price { get; set; } // doke buvo parasyta kad int yra, bet manau klaida
    
    public int Stock { get; set; }
    
    public Guid? ItemGroupId { get; set; }
    
    public ItemGroup? ItemGroup { get; set; }
    
    public ICollection<ItemTax> ItemTaxes { get; set; }
    
    public ICollection<ItemDiscount> ItemDiscounts { get; set; }
}