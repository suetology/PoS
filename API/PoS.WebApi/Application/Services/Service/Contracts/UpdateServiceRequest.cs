using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Service.Contracts;

public class UpdateServiceRequest
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public Guid BusinessId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal? Price { get; set; }

    public int? Duration { get; set; }

    public Guid? EmployeeId { get; set; }
}