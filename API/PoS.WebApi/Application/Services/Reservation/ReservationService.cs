namespace PoS.WebApi.Application.Services.Reservation;

using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Domain.Common;
using Domain.Entities;
using PoS.WebApi.Domain.Enums;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;

    public ReservationService(
        IReservationRepository reservationRepository, 
        IUnitOfWork unitOfWork, 
        IOrderRepository orderRepository)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    public async Task CreateReservation(ReservationDto reservationDto)
    {
        // Validate employee availability
        var availableTimes = await GetAvailableTimesForEmployee(reservationDto.EmployeeId, reservationDto.AppointmentTime.Date);
        if (!availableTimes.Any(t => t == reservationDto.AppointmentTime))
        {
            throw new InvalidOperationException("Selected time slot is not available");
        }

        // Create associated order
        var order = new Order
        {
            Status = OrderStatus.Open,
            Created = DateTime.UtcNow,
            FinalAmount = 0m,
            PaidAmount = 0m,
            TipAmount = 0m,
            EmployeeId = reservationDto.EmployeeId,
            ServiceChargeId = Guid.Parse("F96AFBE4-9169-49C7-AFE7-3D9B375AE1BE"),
            ServiceChargeAmount = 0m
        };

        await _orderRepository.Create(order);
        await _unitOfWork.SaveChanges();

        var reservation = reservationDto.ToDomain(order.Id);
        await _reservationRepository.Create(reservation);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        return await _reservationRepository.GetAll();
    }

    public async Task<Reservation> GetReservationById(Guid id)
    {
        return await _reservationRepository.GetById(id);
    }

    public async Task<IEnumerable<DateTime>> GetAvailableTimesForEmployee(Guid employeeId, DateTime date)
    {
        var existingReservations = await _reservationRepository.GetReservationsByEmployeeAndDate(employeeId, date);
        
        var businessStart = new TimeSpan(9, 0, 0); // 9 AM
        var businessEnd = new TimeSpan(17, 0, 0); // 5 PM
        var appointmentDuration = TimeSpan.FromHours(1);
        
        var availableTimes = new List<DateTime>();
        var currentTime = date.Date + businessStart;
        var endTime = date.Date + businessEnd;

        while (currentTime + appointmentDuration <= endTime)
        {
            var timeSlotEnd = currentTime + appointmentDuration;
            var isSlotAvailable = !existingReservations.Any(r => 
                (r.AppointmentTime >= currentTime && r.AppointmentTime < timeSlotEnd) ||
                r.Status == AppointmentStatus.Booked);

            if (isSlotAvailable)
            {
                availableTimes.Add(currentTime);
            }
            
            currentTime = currentTime.AddHours(1);
        }

        return availableTimes;
    }

    public async Task<bool> CancelReservation(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetById(reservationId);
        if (reservation == null)
            return false;

        reservation.Status = AppointmentStatus.Cancelled;
        await _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        return true;
    }

    public async Task<IEnumerable<Reservation>> GetUpcomingReservations(DateTime startDate, DateTime endDate)
    {
        return await _reservationRepository.GetReservationsInRange(startDate, endDate);
    }

    public async Task SendNotifications()
    {
        var tomorrow = DateTime.UtcNow.Date.AddDays(1);
        var reservations = await _reservationRepository.GetReservationsByDate(tomorrow);

        foreach (var reservation in reservations.Where(r => !r.NotificationSent))
        {
            reservation.NotificationSent = true;
            await _reservationRepository.Update(reservation);
        }

        await _unitOfWork.SaveChanges();
    }

    public async Task<bool> UpdateReservationStatus(Guid reservationId, AppointmentStatus status)
    {
        var reservation = await _reservationRepository.GetById(reservationId);
        if (reservation == null)
            return false;

        reservation.Status = status;
        await _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        return true;
    }
}