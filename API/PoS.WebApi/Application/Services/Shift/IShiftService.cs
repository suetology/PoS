using PoS.WebApi.Application.Services.Shift.Contracts;

namespace PoS.WebApi.Application.Services.Shift
{
    public interface IShiftService
    {
        Task<GetShiftResponse> GetShift(GetShiftRequest request);
        Task CreateShift(CreateShiftRequest request);
        Task<GetAllShiftsResponse> GetShifts(GetAllShiftsRequest request);
        Task DeleteShift(DeleteShiftRequest request);
    }
}
