using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Shift.Contracts;

public class GetAllShiftsRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    [JsonIgnore]
    public QueryParameters QueryParameters { get; set; }
}