namespace PoS.WebApi.Domain.Entities;

public class ItemGroup : Entity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Item> Items { get; set; }
    
    public ICollection<GroupDiscount> GroupDiscounts { get; set; }
}