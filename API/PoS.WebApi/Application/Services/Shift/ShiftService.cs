using PoS.WebApi.Application.Repositories;
using PoS.WebApi.Application.Services.Shift.Contracts;
using PoS.WebApi.Domain.Common;
using PoS.WebApi.Domain.Entities;

namespace PoS.WebApi.Application.Services.Shift
{
    public class ShiftService : IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShiftService(IShiftRepository shiftRepository, IUnitOfWork unitOfWork)
        {
            _shiftRepository = shiftRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetShiftResponse> GetShift(Guid shiftId)
        {
            var shift = await _shiftRepository.Get(shiftId);
            if (shift == null) return null;

            return new GetShiftResponse
            {
                Shift = new ShiftDto
                {
                    Date = shift.Date,
                    StartTime = shift.StartTime.ToString(),
                    EndTime = shift.EndTime.ToString(),
                    EmployeeId = shift.EmployeeId
                }
            };
        }

        public async Task CreateShift(CreateShiftRequest request)
        {
            var shift = new Domain.Entities.Shift
            {
                Date = request.Date,
                StartTime = TimeOnly.Parse(request.StartTime),
                EndTime = TimeOnly.Parse(request.EndTime),
                EmployeeId = request.EmployeeId
            };
                
            await _shiftRepository.Create(shift);
            await _unitOfWork.SaveChanges();
        }

        public async Task<GetShiftsResponse> GetShifts(GetShiftsRequest request)
        {
            var shifts = await _shiftRepository.GetShiftsByFilters(request.EmployeeId, request.FromDate, request.ToDate);
            var shiftsDtos = shifts
                .Select(s => new ShiftDto
                {
                    Date = s.Date,
                    StartTime = s.StartTime.ToString(),
                    EndTime = s.EndTime.ToString(),
                    EmployeeId = s.EmployeeId
                });
            
            return new GetShiftsResponse
            {
                Shifts = shiftsDtos
            };
        }
        public async Task DeleteShift(Guid shiftId)
        {
            await _shiftRepository.Delete(shiftId);
            await _unitOfWork.SaveChanges();
        }
    }
}

