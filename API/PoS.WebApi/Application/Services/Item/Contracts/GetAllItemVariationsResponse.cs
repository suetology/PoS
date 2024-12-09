namespace PoS.WebApi.Application.Services.Item.Contracts;

public class GetAllItemVariationsResponse
{
    public IEnumerable<ItemVariationDto> ItemVariations { get; set; }
}