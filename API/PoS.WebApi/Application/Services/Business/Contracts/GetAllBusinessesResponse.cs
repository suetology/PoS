namespace PoS.WebApi.Application.Services.Business.Contracts;

public class GetAllBusinessesResponse
{
    public IEnumerable<BusinessDto> Businesses { get; set; }
}