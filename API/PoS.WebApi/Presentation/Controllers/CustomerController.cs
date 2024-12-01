using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PoS.WebApi.Application.Services.Customer;
using PoS.WebApi.Application.Services.Customer.Contracts;

namespace PoS.WebApi.Presentation.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer(Guid customerId)
        {
            var response = await _customerService.GetCustomer(customerId);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerService.GetAll();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            if (request == null)
            {
                return BadRequest("Customer data is null.");
            }

            await _customerService.CreateCustomer(request);
            return CreatedAtAction(nameof(GetCustomer), request);
        }
    }
}
