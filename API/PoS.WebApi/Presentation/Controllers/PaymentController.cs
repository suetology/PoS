using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PoS.WebApi.Application.Services.Payments;
using PoS.WebApi.Application.Services.Payments.Contracts;
using PoS.WebApi.Domain.Enums;
using PoS.WebApi.Infrastructure.Security.Extensions;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Events;
using Event = Stripe.V2.Event;
using StripeConfiguration = PoS.WebApi.Infrastructure.Payments.Extensions.StripeConfiguration;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly StripeConfiguration _stripeConfiguration;

    public PaymentController(IPaymentService paymentService, IOptions<StripeConfiguration> stripeConfigOptions)
    {
        _paymentService = paymentService;
        _stripeConfiguration = stripeConfigOptions.Value;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]   
    [HttpPost("card")]
    public async Task<IActionResult> CreateCardPayment([FromBody] CreateCardPaymentRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;
        
        var response = await _paymentService.CreateCardPayment(request);

        Response.Headers.Location = response.SessionUrl;

        return Ok(response.SessionUrl);
    }
    
    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]   
    [HttpPost("cash")]
    public async Task<IActionResult> CreateCashOrGiftCardPayment(CreateCashOrGiftCardPaymentRequest request)
    {
        var businessId = User.GetBusinessId();

        if (businessId == null)
        {
            return Unauthorized("Failed to retrieve Business ID");
        }

        request.BusinessId = businessId.Value;

        await _paymentService.CreateCashOrGiftCardPayment(request);

        return NoContent();
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> HandleStripeWebhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        
        var stripeEvent = EventUtility.ConstructEvent(
            json,
            Request.Headers["Stripe-Signature"],
            _stripeConfiguration.WebhookSecret);
        
        var request = new HandleStripeWebhookRequest
        {
            Event = stripeEvent
        };

        await _paymentService.HandleStripeWebhook(request);
        
        return Ok();
    }
}