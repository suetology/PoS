using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class DeleteShiftRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
}