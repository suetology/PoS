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

        public async Task<GetShiftResponse> GetShift(GetShiftRequest request)
        {
            var shift = await _shiftRepository.Get(request.Id);

            if (shift == null || shift.BusinessId != request.BusinessId)
            {
                return null;
            }

            return new GetShiftResponse
            {
                Shift = new ShiftDto
                {
                    Id = shift.Id,
                    Date = shift.Date.ToString("yyyy-MM-dd"),
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
                BusinessId = request.BusinessId,
                Date = request.Date,
                StartTime = TimeOnly.Parse(request.StartTime),
                EndTime = TimeOnly.Parse(request.EndTime),
                EmployeeId = request.EmployeeId
            };
                
            await _shiftRepository.Create(shift);
            await _unitOfWork.SaveChanges();
        }

        public async Task<GetAllShiftsResponse> GetShifts(GetAllShiftsRequest request)
        {
            var shifts = await _shiftRepository.GetShiftsByFilters(request.QueryParameters.EmployeeId, request.QueryParameters.FromDate, request.QueryParameters.ToDate);
            var shiftsDtos = shifts
                .Where(s => s.BusinessId == request.BusinessId)
                .Select(s => new ShiftDto
                {
                    Id = s.Id,
                    Date = s.Date.ToString("yyyy-MM-dd"),
                    StartTime = s.StartTime.ToString(),
                    EndTime = s.EndTime.ToString(),
                    EmployeeId = s.EmployeeId
                });
            
            return new GetAllShiftsResponse
            {
                Shifts = shiftsDtos
            };
        }
        public async Task DeleteShift(DeleteShiftRequest request)
        {
            var shift = await _shiftRepository.Get(request.Id);

            if (shift == null || shift.BusinessId != request.BusinessId)
            {
                return;
            }
            
            await _shiftRepository.Delete(request.Id);
            await _unitOfWork.SaveChanges();
        }
    }
}

