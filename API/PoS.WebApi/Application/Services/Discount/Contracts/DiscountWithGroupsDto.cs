namespace PoS.WebApi.Application.Services.Discount.Contracts;

public class DiscountWithGroupsDto
{
    public Guid DiscountId { get; set; }
    public string DiscountName { get; set; }
    public decimal Value { get; set; }
    public bool IsPercentage { get; set; }
    public int AmountAvailable { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public List<ItemGroupDto> ItemGroups { get; set; }
}

public class ItemGroupDto
{
    public Guid ItemGroupId { get; set; }
    public string ItemGroupName { get; set; }
}