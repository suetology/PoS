﻿using System;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Customer data is null.");
            }

            await _customerService.CreateCustomer(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.ToDomain().Id }, customerDto);
        }
    }
}
