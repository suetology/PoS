using Newtonsoft.Json;
using Stripe;

namespace PoS.WebApi.Application.Services.Payments.Contracts;

public class HandleStripeWebhookRequest
{
    [JsonIgnore]
    public Event Event { get; set; } 
}