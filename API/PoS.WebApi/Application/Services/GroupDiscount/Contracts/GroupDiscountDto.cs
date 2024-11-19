namespace PoS.WebApi.Application.Services.GroupDiscount.Contracts;

public class GroupDiscountDto
{
    public Guid DiscountId { get; set; }
    public string DiscountName { get; set; }
    public Guid ItemGroupId { get; set; }
    public string ItemGroupName { get; set; }
}
