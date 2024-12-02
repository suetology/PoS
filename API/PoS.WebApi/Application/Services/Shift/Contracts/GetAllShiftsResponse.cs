namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class GetAllShiftsResponse
{
    public IEnumerable<ShiftDto> Shifts { get; set; }
}