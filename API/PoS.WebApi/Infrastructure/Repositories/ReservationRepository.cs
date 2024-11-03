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

    public Task<Reservation> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Reservation>> GetAll()
    {
        return await _dbContext.Reservations
            .Include(r => r.Employee)
            //.Include(r => r.Order)
            .Include(r => r.Customer)
            .ToListAsync();
    }
}
