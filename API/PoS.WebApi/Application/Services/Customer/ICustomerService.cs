namespace PoS.WebApi.Application.Services.Customer;

using PoS.WebApi.Application.Services.Customer.Contracts;
using Domain.Entities;

public interface ICustomerService
{
    Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request);
    Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest customer);
    Task<GetAllCustomersResponse> GetAll(GetAllCustomersRequest request);
}

