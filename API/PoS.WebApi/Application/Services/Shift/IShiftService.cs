using PoS.WebApi.Application.Services.Shift.Contracts;

namespace PoS.WebApi.Application.Services.Shift
{
    public interface IShiftService
    {
        Task<ShiftDto> GetShift(Guid shiftId);
        Task CreateShift(ShiftDto shiftDto);
        Task<IEnumerable<ShiftDto>> GetShifts(Guid? employeeId, DateTime? fromDate, DateTime? toDate);
        Task DeleteShift(Guid shiftId);
    }
}
