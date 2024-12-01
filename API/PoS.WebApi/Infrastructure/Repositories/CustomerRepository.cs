using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DatabaseContext _dbContext;

        public CustomerRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
        }

        public async Task<Customer> Get(Guid id)
        {
           return await _dbContext.Customers.FindAsync(id);
        }
        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _dbContext.Customers.ToListAsync();
        }

        public Task Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
    