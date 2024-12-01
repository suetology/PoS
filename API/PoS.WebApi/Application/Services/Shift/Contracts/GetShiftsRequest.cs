namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class GetShiftsRequest
{
    public Guid? EmployeeId { get; set; }
    
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
}