using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Application.Services.Tax;

using Contracts;
using Domain.Entities;
using PoS.WebApi.Infrastructure.Repositories;
using Repositories;

public class TaxService : ITaxService
{
    private readonly ITaxRepository _taxRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TaxService(
        ITaxRepository taxRepository, 
        IUnitOfWork unitOfWork)
    {
        _taxRepository = taxRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<GetAllTaxesResponse> GetAllTaxes(GetAllTaxesRequest request)
    {
        var taxes = await _taxRepository.GetAll();
        var taxesDtos = taxes
            .Where(t => t.BusinessId == request.BusinessId)
            .Select(t => new TaxDto
            {
                Id = t.Id,
                Name = t.Name,
                Type = t.Type,
                Value = t.Value,
                IsPercentage = t.IsPercentage
            });

        return new GetAllTaxesResponse
        {
            Taxes = taxesDtos
        };
    }

    public async Task<GetTaxResponse> GetTax(GetTaxRequest request)
    {
        var tax = await _taxRepository.Get(request.Id);

        if (tax == null || tax.BusinessId != request.BusinessId)
        {
            return null;
        }

        return new GetTaxResponse
        {
            Tax = new TaxDto
            {
                Id = tax.Id,
                Name = tax.Name,
                Type = tax.Type,
                Value = tax.Value,
                IsPercentage = tax.IsPercentage
            }
        };
    }

    public async Task CreateTax(CreateTaxRequest request)
    {
        var tax = new Tax
        {
            BusinessId = request.BusinessId,
            Name = request.Name,
            Type = request.Type,
            Value = request.Value,
            IsPercentage = request.IsPercentage
        };
        
        await _taxRepository.Create(tax);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateTax(UpdateTaxRequest request)
    {
        var existingTax = await _taxRepository.Get(request.Id);

        if (existingTax == null || existingTax.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Service not found.");
        }

        existingTax.Name = request.Name ?? existingTax.Name;
        existingTax.Type = request.Type ?? existingTax.Type;
        existingTax.Value = request.Value ?? existingTax.Value;
        existingTax.IsPercentage = request.IsPercentage ?? existingTax.IsPercentage;
      
        await _taxRepository.Update(existingTax);
        await _unitOfWork.SaveChanges();
    }
}