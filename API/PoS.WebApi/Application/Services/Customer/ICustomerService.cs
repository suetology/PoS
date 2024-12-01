namespace PoS.WebApi.Application.Services.Customer;

using PoS.WebApi.Application.Services.Customer.Contracts;
using Domain.Entities;

public interface ICustomerService
{
    Task<GetCustomerResponse> GetCustomer(Guid customerId);
    Task CreateCustomer(CreateCustomerRequest customer);
    Task<GetAllCustomersResponse> GetAll();
}

