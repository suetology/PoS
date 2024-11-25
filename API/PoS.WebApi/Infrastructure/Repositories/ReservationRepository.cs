using Microsoft.EntityFrameworkCore;
using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Infrastructure.Persistence;

namespace PoS.WebApi.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly DatabaseContext _dbContext;

    public ReservationRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Reservation reservation)
    {
        await _dbContext.Reservations.AddAsync(reservation);
    }

    public async Task<Reservation> GetById(Guid id)
    {
        return await _dbContext.Reservations
            .Include(r => r.Employee)
            .Include(r => r.Customer)
            .Include(r => r.Order)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetAll()
    {
        return await _dbContext.Reservations
            .Include(r => r.Employee)
            .Include(r => r.Customer)
            .Include(r => r.Order)
            .ToListAsync();
    }

    public async Task Update(Reservation reservation)
    {
        _dbContext.Reservations.Update(reservation);
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByEmployeeAndDate(Guid employeeId, DateTime date)
    {
        return await _dbContext.Reservations
            .Where(r => r.EmployeeId == employeeId 
                && r.AppointmentTime.Date == date.Date 
                && r.Status != Domain.Enums.AppointmentStatus.Cancelled)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByDate(DateTime date)
    {
        return await _dbContext.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Employee)
            .Where(r => r.AppointmentTime.Date == date.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetReservationsInRange(DateTime startDate, DateTime endDate)
    {
        return await _dbContext.Reservations
            .Include(r => r.Customer)
            .Include(r => r.Employee)
            .Include(r => r.Order)
            .Where(r => r.AppointmentTime.Date >= startDate.Date 
                && r.AppointmentTime.Date <= endDate.Date)
            .OrderBy(r => r.AppointmentTime)
            .ToListAsync();
    }

    // Implement the Get method from IRepository interface
    public async Task<Reservation> Get(Guid id)
    {
        return await GetById(id);
    }
}