using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Domain.Entities;

public class ItemVariation : Entity
{
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal AddedPrice { get; set; }
    
    public int Stock { get; set; }
    
    public Guid ItemId { get; set; }
    
    public Item Item { get; set; }
}