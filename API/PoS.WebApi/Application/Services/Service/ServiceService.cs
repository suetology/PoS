using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Application.Services.Service;

using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Service.Contracts;
using PoS.WebApi.Domain.Entities;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Application.Services.User.Contracts;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ServiceService(
        IServiceRepository serviceRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GetServiceResponse> GetService(GetServiceRequest request)
    { 
        var service = await _serviceRepository.Get(request.Id);

        if (service == null || service.BusinessId != request.BusinessId)
        {
            throw new KeyNotFoundException("Service is not found");
        }

        return new GetServiceResponse
        {
            Service = new ServiceDto
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                Duration = service.Duration,
                IsActive = service.IsActive,
                EmployeeId = service.EmployeeId
            }
        };
    }

    public async Task<GetAllServicesResponse> GetServices(GetAllServicesRequest request)
    {
        var services = await _serviceRepository.GetServices(request.Sort, request.Order, request.Page, request.PageSize);
        var serviceDtos = services
            .Where(s => s.BusinessId == request.BusinessId)
            .Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Duration = s.Duration,
                IsActive = s.IsActive,
                EmployeeId = s.EmployeeId
            });

        return new GetAllServicesResponse
        {
            Services = serviceDtos
        };
    }

    public async Task<GetAllServicesResponse> GetActiveServices(GetAllServicesRequest request)
    {
        var services = await _serviceRepository.GetServices(request.Sort, request.Order, request.Page, request.PageSize);
        var serviceDtos = services
            .Where(s => s.BusinessId == request.BusinessId && true == s.IsActive)
            .Select(s => new ServiceDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Duration = s.Duration,
                IsActive = s.IsActive,
                EmployeeId = s.EmployeeId
            });

        return new GetAllServicesResponse
        {
            Services = serviceDtos
        };
    }

    public async Task CreateService(CreateServiceRequest request)
    {
        var service = new Service
        {
            BusinessId = request.BusinessId,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Duration = request.Duration,
            EmployeeId = request.EmployeeId,
            IsActive = true
        };
        
        await _serviceRepository.Create(service);
        await _unitOfWork.SaveChanges();
    }

    public async Task UpdateService(UpdateServiceRequest request)
    {
        var existingService = await _serviceRepository.Get(request.Id);

        if (existingService == null || existingService.BusinessId != request.BusinessId || false == existingService.IsActive)
        {
            throw new KeyNotFoundException("Service not found or unauthorised.");
        }
        
        existingService.Name = request.Name ?? existingService.Name;
        existingService.Description = request.Description ?? existingService.Description;
        existingService.Price = request.Price ?? existingService.Price;
        existingService.Duration = request.Duration ?? existingService.Duration;
        existingService.EmployeeId = request.EmployeeId ?? existingService.EmployeeId;

        await _serviceRepository.Update(existingService);
        await _unitOfWork.SaveChanges();
    }

    public async Task<GetAvailableTimesResponse> GetAvailableTimes(GetAvailableTimesRequest request)
    {
        var service = await _serviceRepository.Get(request.Id);

        var employee = await _userRepository.Get(service.Employee.Id);

        var shift = employee.Shifts
            .FirstOrDefault(s => s.Date.Date == request.Date.Date);

        if (shift == null)
        {
            return new GetAvailableTimesResponse();
        }

        var reservations = employee.Services
            .SelectMany(s => s.Reservations)
            .Where(r => r.AppointmentTime.Date == request.Date.Date && r.Status == AppointmentStatus.Booked)
            .OrderBy(r => r.AppointmentTime.TimeOfDay)
            .ToList();


        Console.WriteLine("Employee services: " + employee.Services.Count);
        Console.WriteLine("Employee reservations: " + reservations.Count);

        var availableTimes = new List<AvailableTimeDto>();

        if (reservations.Count == 0)
        {
            var timeSlot = shift.EndTime - shift.StartTime;

            if (timeSlot.Minutes + timeSlot.Hours * 60 >= service.Duration)
            {
                availableTimes.Add(new AvailableTimeDto
                {
                    Start = shift.StartTime, 
                    End = shift.EndTime.AddMinutes(-service.Duration)
                });
            }
        }
        else
        {
            var timeSlot = TimeOnly.FromDateTime(reservations[0].AppointmentTime) - shift.StartTime;

            if (timeSlot.Minutes + timeSlot.Hours * 60 >= service.Duration)
            {
                availableTimes.Add(new AvailableTimeDto
                {
                    Start = shift.StartTime, 
                    End = TimeOnly.FromDateTime(reservations[0].AppointmentTime).AddMinutes(-service.Duration)
                });
            }

            for (var i = 1; i < reservations.Count; i++)
            {
                timeSlot = TimeOnly.FromDateTime(reservations[i].AppointmentTime) - TimeOnly.FromDateTime(reservations[i - 1].AppointmentTime).AddMinutes(service.Duration);

                if (timeSlot.Minutes + timeSlot.Hours * 60 >= service.Duration)
                { 
                    availableTimes.Add(new AvailableTimeDto
                    {
                        Start = TimeOnly.FromDateTime(reservations[i - 1].AppointmentTime).AddMinutes(service.Duration),
                        End = TimeOnly.FromDateTime(reservations[i].AppointmentTime).AddMinutes(-service.Duration)
                    });
                }
            }

            timeSlot = shift.EndTime - TimeOnly.FromDateTime(reservations[reservations.Count - 1].AppointmentTime).AddMinutes(service.Duration);

            if (timeSlot.Minutes + timeSlot.Hours * 60 >= service.Duration)
            {
                availableTimes.Add(new AvailableTimeDto
                {
                    Start = TimeOnly.FromDateTime(reservations[reservations.Count - 1].AppointmentTime).AddMinutes(service.Duration),
                    End = shift.EndTime.AddMinutes(-service.Duration)
                });
            } 
        }

        return new GetAvailableTimesResponse
        {
            AvailableTimes = availableTimes
        };
    }

    public async Task RetireService(RetireServiceRequest request)
    {
        var existingService = await _serviceRepository.Get(request.Id);
        if (existingService == null || existingService.BusinessId != request.BusinessId || false == existingService.IsActive)
        {
            throw new KeyNotFoundException("Service not found or unauthorised.");
        }

        existingService.IsActive = false;
        
        await _serviceRepository.Update(existingService);
        await _unitOfWork.SaveChanges();
    }

    public async Task RetireEmployeeServices(RetireEmployeeServiceRequest request)
    {
        var services = await _serviceRepository.GetAll();
        var serviceIds = services
            .Where(s => s.BusinessId == request.BusinessId && s.EmployeeId == request.EmployeeId && true == s.IsActive)
            .Select(s => s.Id)
            .ToArray();

        foreach (var id in serviceIds)
        {
            var retireServiceRequest = new RetireServiceRequest
            {
                Id = id,
                BusinessId = request.BusinessId
            };

            await RetireService(retireServiceRequest);
        }
    }
}