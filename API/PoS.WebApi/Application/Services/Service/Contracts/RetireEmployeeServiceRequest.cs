using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.User.Contracts;

public class RetireEmployeeServiceRequest
{
    [JsonIgnore]
    public Guid BusinessId { get; set; }

    public Guid EmployeeId { get; set; }
}