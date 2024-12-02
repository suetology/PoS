namespace PoS.WebApi.Application.Services.Service.Contracts;

public class GetAllServicesResponse
{
    public IEnumerable<ServiceDto> Services { get; set; }
}