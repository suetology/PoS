namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class GetShiftsResponse
{
    public IEnumerable<ShiftDto> Shifts { get; set; }
}