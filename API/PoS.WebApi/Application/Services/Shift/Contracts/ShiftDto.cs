namespace PoS.WebApi.Application.Services.Shift.Contracts;
using PoS.WebApi.Domain.Entities;

public class ShiftDto
{
    public Guid Id { get; set; }
    
    public string Date { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public Guid EmployeeId { get; set; }
}
