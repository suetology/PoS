namespace PoS.WebApi.Application.Services.Business;

using Contracts;
using Domain.Entities;
using PoS.WebApi.Domain.Common;
using Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BusinessService : IBusinessService
{
    private readonly IBusinessRepository _businessRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BusinessService(IBusinessRepository businessRepository, IUnitOfWork unitOfWork)
    {
        _businessRepository = businessRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateBusiness(BusinessDto businessDto)
    {
        var business = businessDto.ToDomain();

        await _businessRepository.Create(business);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Business>> GetAllBusiness()
    {
        return await _businessRepository.GetAll();
    }

    public async Task<Business> GetBusiness(Guid businessId)
    {
        return await _businessRepository.Get(businessId);
    }

    public async Task<bool> UpdateBusiness(Guid businessId, BusinessDto businessDto)
    {
        var existingBusiness = await _businessRepository.Get(businessId);

        if (existingBusiness == null)
        {
            return false;
        }

        var updatedBusiness = businessDto.ToDomain();
        existingBusiness.Update(updatedBusiness);

        _businessRepository.Update(existingBusiness);
        await _unitOfWork.SaveChanges();

        return true;

    }
}
