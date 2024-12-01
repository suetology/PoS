namespace PoS.WebApi.Application.Services.Tax.Contracts;

public class GetAllTaxesResponse
{
    public IEnumerable<TaxDto> Taxes { get; set; }
}