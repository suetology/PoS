namespace PoS.WebApi.Application.Services.Tax;

using Application.Services.Tax.Contracts;

public interface ITaxService
{
    Task<GetAllTaxesResponse> GetAllTaxes(GetAllTaxesRequest request);

    Task<GetAllTaxesResponse> GetAllValidTaxes(GetAllTaxesRequest request);

    Task<GetTaxResponse> GetTax(GetTaxRequest request);
    
    Task CreateTax(CreateTaxRequest request);
    Task UpdateTax(UpdateTaxRequest request);
}