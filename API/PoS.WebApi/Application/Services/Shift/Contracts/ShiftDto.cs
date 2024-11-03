namespace PoS.WebApi.Application.Services.Shift.Contracts;
using PoS.WebApi.Domain.Entities;

public class ShiftDto
{
    public DateTime Date { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public Guid EmployeeId { get; set; }

    public Shift ToDomain() 
    {
        return new Shift()
        {
            Date = this.Date,
            StartTime = TimeOnly.Parse(StartTime),
            EndTime = TimeOnly.Parse(EndTime),
            EmployeeId = this.EmployeeId
        };
    }
}
