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

    public async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request)
    {
        var customer = await _customerRepository.Get(request.Id);

        if (customer.BusinessId != request.BusinessId)
        {
            return null;
        }

        return new GetCustomerResponse
        {
            Customer = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            }
        };
    }
    
    public async Task<CreateCustomerResponse> CreateCustomer(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            BusinessId = request.BusinessId,
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        await _customerRepository.Create(customer);
        await _unitOfWork.SaveChanges();

        return new CreateCustomerResponse
        {
            Id = customer.Id
        };
    }

    public async Task<GetAllCustomersResponse> GetAll(GetAllCustomersRequest request)
    {
        var customers =  await _customerRepository.GetAll();
        var customersDtos = customers
            .Where(c => c.BusinessId == request.BusinessId)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
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

