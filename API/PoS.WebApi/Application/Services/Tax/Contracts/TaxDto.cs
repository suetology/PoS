namespace PoS.WebApi.Application.Services.Tax.Contracts;

using Domain.Entities;
using Domain.Enums;

public class TaxDto
{
    public string Name { get; set; }
    
    public TaxType Type { get; set; }
    
    public decimal Value { get; set; }

    public bool IsPercentage { get; set; } = true;
}