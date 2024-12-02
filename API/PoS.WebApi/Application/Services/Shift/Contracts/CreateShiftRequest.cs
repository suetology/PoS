using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class CreateShiftRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public DateTime Date { get; set; }

    public string StartTime { get; set; }

    public string EndTime { get; set; }

    public Guid EmployeeId { get; set; }
}