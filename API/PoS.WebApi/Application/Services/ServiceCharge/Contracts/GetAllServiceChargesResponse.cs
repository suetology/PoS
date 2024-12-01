namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

public class GetAllServiceChargesResponse
{
    public IEnumerable<ServiceChargeDto> ServiceCharges { get; set; }
}