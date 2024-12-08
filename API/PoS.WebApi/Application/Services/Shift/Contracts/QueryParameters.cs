namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class QueryParameters
{
    public Guid? EmployeeId { get; set; }

    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
}