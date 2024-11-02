namespace PoS.WebApi.Application.Services.Business;

using Domain.Entities;
using PoS.WebApi.Application.Services.Business.Contracts;

public interface IBusinessService
{
    Task<IEnumerable<Business>> GetAllBusiness();
    Task<Business> GetBusiness(Guid businessId);
    Task CreateBusiness(BusinessDto business);
    Task<bool> UpdateBusiness(Guid id, BusinessDto request);
}