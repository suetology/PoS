using Stripe;

namespace PoS.WebApi.Infrastructure.Payments.Extensions;

public static class StripeExtensions
{
    public static WebApplicationBuilder ConfigureStripe(this WebApplicationBuilder builder)
    {
        var stripeSection = builder.Configuration.GetSection("Stripe");
        var stripeConfiguration = stripeSection.Get<StripeConfiguration>();

        Stripe.StripeConfiguration.ApiKey = stripeConfiguration.ApiKey;

        return builder;
    }
}