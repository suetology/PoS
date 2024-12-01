using PoS.WebApi.Application.Services.Shift.Contracts;

namespace PoS.WebApi.Application.Services.Shift
{
    public interface IShiftService
    {
        Task<GetShiftResponse> GetShift(Guid shiftId);
        Task CreateShift(CreateShiftRequest request);
        Task<GetShiftsResponse> GetShifts(GetShiftsRequest request);
        Task DeleteShift(Guid shiftId);
    }
}
