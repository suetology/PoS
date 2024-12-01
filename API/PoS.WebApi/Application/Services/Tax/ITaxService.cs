namespace PoS.WebApi.Application.Services.Tax;

using Application.Services.Tax.Contracts;
using Domain.Entities;

public interface ITaxService
{
    Task<GetAllTaxesResponse> GetAllTaxes();

    Task<GetTaxResponse> GetTax(Guid taxId);
    
    Task CreateTax(CreateTaxRequest request);
}