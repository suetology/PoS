using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly DatabaseContext _dbContext;

        public ShiftRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shift> Get(Guid id) => await _dbContext.Shifts.FindAsync(id);

        public async Task<IEnumerable<Shift>> GetAll() => await _dbContext.Shifts.ToListAsync();

        public async Task Create(Shift shift) => await _dbContext.Shifts.AddAsync(shift);

        public async Task<IEnumerable<Shift>> GetShiftsByFilters(Guid? employeeId, DateTime? fromDate, DateTime? toDate)
        {
            var query = _dbContext.Shifts.AsQueryable();

            if (employeeId.HasValue)
            {
                query = query.Where(s => s.EmployeeId == employeeId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(s => s.Date >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(s => s.Date <= toDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task Delete(Guid id)
        {
            var shift = await _dbContext.Shifts.FindAsync(id);
            if (shift != null)
            {
                _dbContext.Shifts.Remove(shift);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
