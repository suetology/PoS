using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Application.Services.Customer;

using Contracts;
using Domain.Entities;
using PoS.WebApi.Infrastructure.Repositories;
using Repositories;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CustomerService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCustomerResponse> GetCustomer(Guid customerId)
    {
        var customer = await _customerRepository.Get(customerId);

        return new GetCustomerResponse
        {
            Customer = new CustomerDto
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            }
        };
    }
    
    public async Task CreateCustomer(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        await _customerRepository.Create(customer);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllCustomersResponse> GetAll()
    {
        var customers =  await _customerRepository.GetAll();
        var customersDtos = customers
            .Select(c => new CustomerDto
            {
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            });

        return new GetAllCustomersResponse
        {
            Customers = customersDtos
        };
    }
}

