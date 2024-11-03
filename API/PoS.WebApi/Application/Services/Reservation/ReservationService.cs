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

    public ReservationService(IReservationRepository reservationRepository, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
    }

    public async Task CreateReservation(ReservationDto reservationDto)
    {
        Guid orderId;
        // Order MOCK kad gaut OrderId
        var mockOrder = new Order
        {
            Status = OrderStatus.Open,
            Created = DateTime.UtcNow,
            Closed = DateTime.UtcNow.AddHours(1),
            FinalAmount = 0m,
            PaidAmount = 0m,
            TipAmount = 0m,
            EmployeeId = Guid.Parse("f8fb2cb2-6119-4f42-8954-412e8bdc2351"),
            ServiceChargeId = Guid.Parse("F96AFBE4-9169-49C7-AFE7-3D9B375AE1BE"),
            ServiceChargeAmount = 0m
        };

        // Save the mock order to the database
        await _orderRepository.Create(mockOrder);
        await _unitOfWork.SaveChanges();

        orderId = mockOrder.Id;

        var reservation = reservationDto.ToDomain(orderId);

        await _reservationRepository.Create(reservation);
        await _unitOfWork.SaveChanges();
    }

    public async Task<IEnumerable<Reservation>> GetAllReservations()
    {
        return await _reservationRepository.GetAll();
    }
}
