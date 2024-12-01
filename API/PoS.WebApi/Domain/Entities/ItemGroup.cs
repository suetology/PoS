using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class ItemGroup : Entity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Item> Items { get; set; } = new List<Item>();

    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}