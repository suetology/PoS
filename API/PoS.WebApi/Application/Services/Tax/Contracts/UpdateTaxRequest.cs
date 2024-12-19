using PoS.WebApi.Domain.Enums;
using System.Text.Json.Serialization;

namespace PoS.WebApi.Application.Services.Tax.Contracts
{
    public class UpdateTaxRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid BusinessId { get; set; }
        
        public string Name { get; set; }

        public decimal? Value { get; set; }

        public bool? IsPercentage { get; set; } = true;
    }
}
