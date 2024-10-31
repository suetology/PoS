namespace PoS.WebApi.Domain.Entities;

public class ItemDiscount : Entity
{
    public Guid DiscountId { get; set; }
    
    public Discount Discount { get; set; }
    
    public Guid ItemId { get; set; }
    
    public Item Item { get; set; }
}