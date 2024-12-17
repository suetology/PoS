using Stripe;

namespace PoS.WebApi.Infrastructure.Payments.Extensions;

public static class StripeExtensions
{
    public static WebApplicationBuilder ConfigureStripe(this WebApplicationBuilder builder)
    {
        var stripeSection = builder.Configuration.GetSection("Stripe");
        builder.Services.Configure<Extensions.StripeConfiguration>(stripeSection);
        
        var stripeConfiguration = stripeSection.Get<StripeConfiguration>();

        Stripe.StripeConfiguration.ApiKey = stripeConfiguration.ApiKey;

        return builder;
    }
}