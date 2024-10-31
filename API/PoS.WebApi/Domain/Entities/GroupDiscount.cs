namespace PoS.WebApi.Domain.Entities;

public class GroupDiscount : Entity
{
    public Guid DiscountId { get; set; }
    
    public Discount Discount { get; set; }
    
    public Guid ItemGroupId { get; set; }
    
    public ItemGroup ItemGroup { get; set; }
}