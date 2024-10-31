namespace PoS.WebApi.Domain.Entities;

public class ItemTax : Entity
{
    public Guid TaxId { get; set; }
    
    public Tax Tax { get; set; }
    
    public Guid ItemId { get; set; }
    
    public Item Item { get; set; }
}