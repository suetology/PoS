namespace PoS.WebApi.Application.Services.ItemGroup.Contracts;

using Domain.Entities;
using PoS.WebApi.Application.Services.Discount.Contracts;

public class ItemGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<DiscountDto> Discounts { get; set; } = new List<DiscountDto>();
}