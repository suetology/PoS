using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DatabaseContext _dbContext;

        public ServiceRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Service> Get(Guid id) =>
            await _dbContext.Services
                .Include(s => s.Employee)
                    .ThenInclude(u => u.Shifts)
                .Include(s => s.Reservations)
                .FirstOrDefaultAsync(s => s.Id == id);

        public async Task Create(Service service)
        {
            await _dbContext.Services.AddAsync(service);
        }

        public async Task<IEnumerable<Service>> GetAll() =>
            await _dbContext.Services.ToListAsync();

        public async Task<IEnumerable<Service>> GetServices(string sort, string order, int page, int pageSize)
        {
            IQueryable<Service> query = _dbContext.Services;

            if (!string.IsNullOrEmpty(sort))
            {
                query = sort switch
                {
                    "name" => order == "asc" ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
                    "price" => order == "asc" ? query.OrderBy(s => s.Price) : query.OrderByDescending(s => s.Price),
                    "duration" => order == "asc" ? query.OrderBy(s => s.Duration) : query.OrderByDescending(s => s.Duration),
                    "isActive" => order == "asc" ? query.OrderBy(s => s.IsActive) : query.OrderByDescending(s => s.IsActive),
                    "associatedEmployeeID" => order == "asc" ? query.OrderBy(s => s.EmployeeId) : query.OrderByDescending(s => s.EmployeeId),
                    _ => query
                };
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Update(Service service)
        {
            _dbContext.Services.Update(service);
        }
    }
}
