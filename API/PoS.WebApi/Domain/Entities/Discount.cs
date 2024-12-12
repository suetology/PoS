using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Discount : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;
    
    public int AmountAvailable { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; }
    
    public Order Order { get; set; }
    
    public ICollection<ItemGroup> ItemGroups { get; set; } = new List<ItemGroup>();
    
    public ICollection<Item> Items { get; set; } = new List<Item>();
}