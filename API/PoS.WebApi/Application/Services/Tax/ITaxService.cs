namespace PoS.WebApi.Application.Services.Tax;

using Application.Services.Tax.Contracts;
using Domain.Entities;

public interface ITaxService
{
    Task<GetAllTaxesResponse> GetAllTaxes(GetAllTaxesRequest request);

    Task<GetTaxResponse> GetTax(GetTaxRequest request);
    
    Task CreateTax(CreateTaxRequest request);
}