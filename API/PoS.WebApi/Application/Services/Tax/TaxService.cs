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
                IsPercentage = t.IsPercentage,
                Retired = t.Retired
            });

        return new GetAllTaxesResponse
        {
            Taxes = taxesDtos
        };
    }

    public async Task<GetAllTaxesResponse> GetAllValidTaxes(GetAllTaxesRequest request)
    {
        var taxes = await _taxRepository.GetAll();
        var taxesDtos = taxes
            .Where(t => t.BusinessId == request.BusinessId && false == t.Retired)
            .Select(t => new TaxDto
            {
                Id = t.Id,
                Name = t.Name,
                Type = t.Type,
                Value = t.Value,
                IsPercentage = t.IsPercentage,
                Retired = t.Retired
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
            throw new KeyNotFoundException("Tax is not found");
        }

        return new GetTaxResponse
        {
            Tax = new TaxDto
            {
                Id = tax.Id,
                Name = tax.Name,
                Type = tax.Type,
                Value = tax.Value,
                IsPercentage = tax.IsPercentage,
                Retired = tax.Retired,
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
            IsPercentage = request.IsPercentage,
            Retired = false,
        };
        
        await _taxRepository.Create(tax);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateTax(UpdateTaxRequest request)
    {
        var existingTax = await _taxRepository.Get(request.Id);

        if (existingTax == null || existingTax.BusinessId != request.BusinessId || true == existingTax.Retired)
        {
            throw new KeyNotFoundException("Tax not found or unauthorised.");
        }

        var updatedTax = new Tax
        {
            BusinessId = existingTax.BusinessId,
            Name = request.Name,
            Type = existingTax.Type,
            Value = request.Value ?? existingTax.Value,
            IsPercentage = request.IsPercentage ?? existingTax.IsPercentage,
            Retired = false,
        };
        await _taxRepository.Create(updatedTax);    

        existingTax.Retired = true;
        await _taxRepository.Update(existingTax);
        
        await _unitOfWork.SaveChanges();
    }

    public async Task RetireTax(RetireTaxRequest request)
    {
        var existingTax = await _taxRepository.Get(request.Id);
        if (existingTax == null || existingTax.BusinessId != request.BusinessId || true == existingTax.Retired)
        {
            throw new KeyNotFoundException("Tax not found or unauthorised.");
        }

        existingTax.Retired = true;

        await _taxRepository.Update(existingTax);
        await _unitOfWork.SaveChanges();
    }
}