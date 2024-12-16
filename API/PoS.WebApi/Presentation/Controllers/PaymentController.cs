using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Payments;
using PoS.WebApi.Application.Services.Payments.Contracts;
using PoS.WebApi.Domain.Enums;

namespace PoS.WebApi.Presentation.Controllers;

[ApiController]
[Route("payments")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [Authorize(Roles = $"{nameof(Role.SuperAdmin)},{nameof(Role.BusinessOwner)},{nameof(Role.Employee)}")]   
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionRequest request)
    {
        var response = await _paymentService.CreateCheckoutSession(request);

        Response.Headers.Location = response.SessionUrl;
        
        Console.WriteLine(response.SessionUrl);

        return Redirect(response.SessionUrl);
    }

    [HttpPost("complete-checkout")]
    public async Task<IActionResult> CompleteCheckout()
    {
        Console.WriteLine("\n\n\n Hook triggered \n\n\n");
        
        return Ok();
    }
}