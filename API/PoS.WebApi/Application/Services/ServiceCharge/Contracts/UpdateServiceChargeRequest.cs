using System.Text.Json.Serialization;

public class UpdateServiceChargeRequest
{

    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid BusinessId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Value { get; set; }
    public bool? IsPercentage { get; set; }
}
