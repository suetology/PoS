namespace PoS.WebApi.Application.Services.Tax;

using Application.Services.Tax.Contracts;
using Domain.Entities;

public interface ITaxService
{
    Task<IEnumerable<Tax>> GetAllTaxes();

    Task<Tax> GetTax(Guid taxId);
    
    Task CreateTax(TaxDto tax);
}