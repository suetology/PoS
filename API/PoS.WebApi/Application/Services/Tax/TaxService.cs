using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Application.Services.Tax;

using Contracts;
using Domain.Entities;
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
    
    public async Task<Tax> GetTax(Guid taxId)
    {
        return await _taxRepository.Get(taxId);
    }

    public async Task CreateTax(TaxDto taxDto)
    {
        var tax = taxDto.ToDomain();
        
        await _taxRepository.Create(tax);
        await _unitOfWork.SaveChanges();
    }
}