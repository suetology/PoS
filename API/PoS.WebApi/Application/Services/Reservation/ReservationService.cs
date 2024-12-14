namespace PoS.WebApi.Application.Services.Reservation;

using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Reservation.Contracts;
using PoS.WebApi.Domain.Common;
using Domain.Entities;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Application.Services.Service.Contracts;

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
            BusinessId = request.BusinessId,
            NotificationSent = false,
            Status = AppointmentStatus.Booked,
            ReservationTime = DateTime.UtcNow,
            AppointmentTime = request.AppointmentTime,
            ServiceId = request.ServiceId,
            OrderId = request.OrderId
        };
        
        await _reservationRepository.Create(reservation);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAllReservationsResponse> GetAllReservations(GetAllReservationsRequest request)
    {
        var reservations = await _reservationRepository.GetAll();
        var reservationDtos = reservations
            .Where(r => r.BusinessId == request.BusinessId)
            .Select(r => new ReservationDto
            {
                Id = r.Id,
                NotificationSent = r.NotificationSent,
                Status = r.Status,
                ReservationTime = r.ReservationTime,
                AppointmentTime = r.AppointmentTime,    
                Service = new ServiceDto
                {
                    Id = r.Service.Id,
                    Name = r.Service.Name,
                    Description = r.Service.Description,
                    Price = r.Service.Price,
                    Duration = r.Service.Duration,
                    IsActive = r.Service.IsActive,
                    EmployeeId = r.Service.EmployeeId
                }
            });

        return new GetAllReservationsResponse
        {
            Reservations = reservationDtos
        };
    }

    public async Task<GetReservationResponse> GetReservationById(GetReservationRequest request)
    {
        var reservation = await _reservationRepository.GetById(request.Id);

        if (reservation.BusinessId != request.BusinessId)
        {
            return null;
        }
        
        return new GetReservationResponse
        {
            Reservation = new ReservationDto
            {
                Id = reservation.Id,
                NotificationSent = reservation.NotificationSent,
                Status = reservation.Status,
                ReservationTime = reservation.ReservationTime,
                AppointmentTime = reservation.AppointmentTime,
                Service = new ServiceDto
                {
                    Id = reservation.Service.Id,
                    Name = reservation.Service.Name,
                    Description = reservation.Service.Description,
                    Price = reservation.Service.Price,
                    Duration = reservation.Service.Duration,
                    IsActive = reservation.Service.IsActive,
                    EmployeeId = reservation.Service.EmployeeId
                }
            }
        };
    }

    public async Task<bool> UpdateReservation(UpdateReservationRequest request)
    {
        var reservation = await _reservationRepository.GetById(request.Id);

        if (reservation == null || reservation.BusinessId != request.BusinessId)
        {
            return false;
        }
        
        reservation.AppointmentTime = request.AppointmentTime ?? reservation.AppointmentTime;
        reservation.Status = request.Status ?? reservation.Status;
        
        await _reservationRepository.Update(reservation);
        await _unitOfWork.SaveChanges();

        return true;
    }
    
    public async Task CancelReservation(CancelReservationRequest request)
    {
        var reservation = await _reservationRepository.GetById(request.ReservationId);

        if (reservation == null || reservation.BusinessId != request.BusinessId)
        {
            return;
        }

        reservation.Status = AppointmentStatus.Cancelled;

        await _unitOfWork.SaveChanges();
    }
}