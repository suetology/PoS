namespace PoS.WebApi.Application.Services.ServiceCharge.Contracts;

public class CreateServiceChargeRequest
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Value { get; set; }
    
    public bool IsPercentage { get; set; }
}