namespace PoS.WebApi.Application.Services.Business;

using Domain.Entities;
using PoS.WebApi.Application.Services.Business.Contracts;

public interface IBusinessService
{
    Task<GetAllBusinessesResponse> GetAllBusiness();
    Task<GetBusinessResponse> GetBusiness(GetBusinessRequest request);
    Task CreateBusiness(CreateBusinessRequest request);
    Task<bool> UpdateBusiness(UpdateBusinessRequest request);
}