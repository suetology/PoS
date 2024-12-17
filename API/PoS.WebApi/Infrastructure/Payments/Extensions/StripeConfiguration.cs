namespace PoS.WebApi.Infrastructure.Payments.Extensions;

public class StripeConfiguration
{
    public string ApiKey { get; set; }
    
    public string WebhookSecret { get; set; }
}