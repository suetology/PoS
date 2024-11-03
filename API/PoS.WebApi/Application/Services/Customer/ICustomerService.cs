namespace PoS.WebApi.Application.Services.Customer;

using PoS.WebApi.Application.Services.Customer.Contracts;
using Domain.Entities;

public interface ICustomerService
{
    Task<Customer> GetCustomer(Guid customerId);
    Task CreateCustomer(CustomerDto customer);
    Task<IEnumerable<Customer>> GetAll();
}

