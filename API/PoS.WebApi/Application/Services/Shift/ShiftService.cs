using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ShiftDto> GetShift(Guid shiftId)
        {
            var shift = await _shiftRepository.Get(shiftId);
            if (shift == null) return null;

            return new ShiftDto
            {
                Date = shift.Date,
                StartTime = shift.StartTime.ToString(),
                EndTime = shift.EndTime.ToString(),
                EmployeeId = shift.EmployeeId
            };
        }

        public async Task CreateShift(ShiftDto shiftDto)
        {
            var shift = shiftDto.ToDomain();
            await _shiftRepository.Create(shift);
            await _unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<ShiftDto>> GetShifts(Guid? employeeId, DateTime? fromDate, DateTime? toDate)
        {
            var shifts = await _shiftRepository.GetShiftsByFilters(employeeId, fromDate, toDate);
            return shifts.Select(s => new ShiftDto
            {
                Date = s.Date,
                StartTime = s.StartTime.ToString(),
                EndTime = s.EndTime.ToString(),
                EmployeeId = s.EmployeeId
            });
        }
        public async Task DeleteShift(Guid shiftId)
        {
            await _shiftRepository.Delete(shiftId);
            await _unitOfWork.SaveChanges();
        }
    }
}

