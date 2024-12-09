namespace PoS.WebApi.Application.Services.Item.Contracts;

public class ItemVariationDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal AddedPrice { get; set; }
    
    public int Stock { get; set; }
    
    public Guid ItemId { get; set; }
}