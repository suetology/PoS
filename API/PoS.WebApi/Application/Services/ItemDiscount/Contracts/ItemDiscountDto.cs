namespace PoS.WebApi.Application.Services.ItemDiscount.Contracts;

public class ItemDiscountDto
{
    public Guid ItemId { get; set; }
    public string ItemName { get; set; }
    public Guid DiscountId { get; set; }
    public string DiscountName { get; set; }
}
