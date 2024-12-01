namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class CreateShiftRequest
{
    public DateTime Date { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public Guid EmployeeId { get; set; }
}