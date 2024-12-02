using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class GetAllShiftsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public Guid? EmployeeId { get; set; }
    
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
}