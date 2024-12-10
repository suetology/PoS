using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Repositories;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetReservationsByDate(DateTime date);
    Task<IEnumerable<Reservation>> GetReservationsInRange(DateTime startDate, DateTime endDate);
    Task<Reservation> GetById(Guid id);
}