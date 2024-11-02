using PoS.WebApi.Domain.Common;

namespace PoS.WebApi.Application.Services.Customer;

using Contracts;
using Domain.Entities;
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

        public async Task<Customer> GetCustomer(Guid customerId)
        {
              return await _customerRepository.Get(customerId);
        }


        public async Task CreateCustomer(CustomerDto customerDto)
        {
            var customer = customerDto.ToDomain();

            await _customerRepository.Create(customer);
            await _unitOfWork.SaveChanges();
        }
}

