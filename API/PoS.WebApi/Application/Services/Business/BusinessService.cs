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

    public async Task CreateBusiness(CreateBusinessRequest request)
    {
        var business = new Business
        {
            Name = request.Name,
            Address = request.Address,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        await _businessRepository.Create(business);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllBusinessesResponse> GetAllBusiness()
    {
        var businesses = await _businessRepository.GetAll();
        var businessDtos = businesses
            .Select(b => new BusinessDto
            {
                Name = b.Name,
                Address = b.Address,
                Email = b.Email,
                PhoneNumber = b.PhoneNumber
            });
        
        return new GetAllBusinessesResponse
        {
            Businesses = businessDtos
        };
    }

    public async Task<GetBusinessResponse> GetBusiness(GetBusinessRequest request)
    {
        var business = await _businessRepository.Get(request.Id);

        return new GetBusinessResponse
        {
            Business = new BusinessDto
            {                
                Name = business.Name,
                Address = business.Address,
                Email = business.Email,
                PhoneNumber = business.PhoneNumber
            }
        };
    }

    public async Task<bool> UpdateBusiness(UpdateBusinessRequest request)
    {
        var existingBusiness = await _businessRepository.Get(request.Id);

        if (existingBusiness == null)
        {
            return false;
        }
        
        existingBusiness.Name = request.Name ?? existingBusiness.Name;
        existingBusiness.Address = request.Address ?? existingBusiness.Address;
        existingBusiness.Email = request.Email ?? existingBusiness.Email;
        existingBusiness.PhoneNumber = request.PhoneNumber ?? existingBusiness.PhoneNumber;

        await _businessRepository.Update(existingBusiness);
        await _unitOfWork.SaveChanges();

        return true;
    }
}
