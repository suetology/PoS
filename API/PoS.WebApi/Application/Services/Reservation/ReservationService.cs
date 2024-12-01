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

    public async Task CreateReservation(CreateReservationRequest request)
    {
        // Validate employee availability
        /*var availableTimes = await GetAvailableTimesForEmployee(request.EmployeeId, request.AppointmentTime.Date);
        if (!availableTimes.Any(t => t == request.AppointmentTime))
        {
            throw new InvalidOperationException("Selected time slot is not available");
        }*/

        var reservation = new Reservation
        {
            NotificationSent = false,
            Status = AppointmentStatus.Booked,
            ReservationTime = DateTime.UtcNow,
            AppointmentTime = request.AppointmentTime,
            CustomerId = request.CustomerId,
            OrderId = request.OrderId,
            EmployeeId = request.EmployeeId
        };
        
        await _reservationRepository.Create(reservation);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllReservationsResponse> GetAllReservations()
    {
        var reservations = await _reservationRepository.GetAll();
        var reservationDtos = reservations
            .Select(r => new ReservationDto
            {
                NotificationSent = r.NotificationSent,
                Status = r.Status,
                ReservationTime = r.ReservationTime,
                AppointmentTime = r.AppointmentTime,
                CustomerId = r.CustomerId,
                EmployeeId = r.EmployeeId
            });

        return new GetAllReservationsResponse
        {
            Reservations = reservationDtos
        };
    }

    public async Task<GetReservationResponse> GetReservationById(Guid id)
    {
        var reservation = await _reservationRepository.GetById(id);

        return new GetReservationResponse
        {
            Reservation = new ReservationDto
            {
                NotificationSent = reservation.NotificationSent,
                Status = reservation.Status,
                ReservationTime = reservation.ReservationTime,
                AppointmentTime = reservation.AppointmentTime,
                CustomerId = reservation.CustomerId,
                EmployeeId = reservation.EmployeeId
            }
        };
    }

    public async Task<bool> UpdateReservation(Guid id, UpdateReservationRequest request)
    {
        var reservation = await _reservationRepository.GetById(id);

        if (reservation == null)
        {
            return false;
        }
        
        reservation.AppointmentTime = request.AppointmentTime ?? reservation.AppointmentTime;
        reservation.Status = request.Status ?? reservation.Status;
        
        await _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();

        return true;
    }
    
    /*public async Task<IEnumerable<DateTime>> GetAvailableTimesForEmployee(Guid employeeId, DateTime date)
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
    }*/

    /*public async Task<bool> CancelReservation(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetById(reservationId);
        if (reservation == null)
            return false;

        reservation.Status = AppointmentStatus.Cancelled;
        await _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        return true;
    }*/

    /*public async Task<IEnumerable<Reservation>> GetUpcomingReservations(DateTime startDate, DateTime endDate)
    {
        return await _reservationRepository.GetReservationsInRange(startDate, endDate);
    }*/

    /*public async Task<bool> UpdateReservationStatus(Guid reservationId, AppointmentStatus status)
    {
        var reservation = await _reservationRepository.GetById(reservationId);
        if (reservation == null)
            return false;

        reservation.Status = status;
        await _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();
        return true;
    }*/
}