using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class Discount : Entity
{
    public string Name { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;
    
    public int AmountAvailable { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; }
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public ICollection<ItemGroup> ItemGroups { get; set; } = new List<ItemGroup>();
    
    public ICollection<Item> Items { get; set; } = new List<Item>();
}