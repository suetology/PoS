using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class GetShiftRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}