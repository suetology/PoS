﻿using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Application.Services.Customer;

using Contracts;
using Domain.Entities;
using PoS.WebApi.Application.Services.Order;
using PoS.WebApi.Application.Services.Order.Contracts;
using PoS.WebApi.Infrastructure.Repositories;
using Repositories;
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CustomerService(
    ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCustomerResponse> GetCustomer(GetCustomerRequest request)
    {
        var customer = await _customerRepository.Get(request.Id);

        if (customer == null || customer.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Customer is not found");
        }

        return new GetCustomerResponse
        {
            Customer = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Retired = customer.Retired
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
            PhoneNumber = request.PhoneNumber,
            Retired = false,
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
                PhoneNumber = c.PhoneNumber,
                Retired = c.Retired
            });

        return new GetAllCustomersResponse
        {
            Customers = customersDtos
        };
    }

    public async Task<GetAllCustomersResponse> GetAllActive(GetAllCustomersRequest request)
    {
        var customers =  await _customerRepository.GetAll();
        var customersDtos = customers
            .Where(c => c.BusinessId == request.BusinessId && false == c.Retired)
            .Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Retired = c.Retired
            });

        return new GetAllCustomersResponse
        {
            Customers = customersDtos
        };
    }

    public async Task UpdateCustomer(UpdateCustomerRequest request)
    {
        var existingCustomer = await _customerRepository.Get(request.Id);
        if (existingCustomer == null || existingCustomer.BusinessId != request.BusinessId || true == existingCustomer.Retired)
        {
            throw new KeyNotFoundException("Customer not found or unauthorised.");
        }

        existingCustomer.Name = request.Name;
        existingCustomer.Email = request.Email;
        existingCustomer.PhoneNumber = request.PhoneNumber;

        await _customerRepository.Update(existingCustomer);
        await _unitOfWork.SaveChanges();
    }

    public async Task RetireCustomer(RetireCustomerRequest request)
    {
        var existingCustomer = await _customerRepository.Get(request.Id);
        if (existingCustomer == null || existingCustomer.BusinessId != request.BusinessId || true == existingCustomer.Retired)
        {
            throw new KeyNotFoundException("Customer not found or unauthorised.");
        }
        
        existingCustomer.Retired = true;
        
        await _customerRepository.Update(existingCustomer);
        await _unitOfWork.SaveChanges();
    }
}

