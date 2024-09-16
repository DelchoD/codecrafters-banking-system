﻿using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Models.Customer;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Utils;

namespace BankingManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace BankingManagementSystem.Controllers
    {
        [ApiController]
        [Route("api/customers")]
        public class CustomerController : ControllerBase
        {
            private readonly ICustomerService _customerService;
            private readonly IAccountService _accountService;

            public CustomerController(ICustomerService customerService, IAccountService accountService)
            {
                _customerService = customerService;
                _accountService = accountService;
            }

            [HttpGet("{id}")]
            [Authorize(Roles = "User,Admin")]
            public async Task<ActionResult<AllDto>> GetCustomerById(string id)
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null) return NotFound();

                return Ok(EntityMappers.ToCustomerAllDto(customer));
            }

            [HttpGet]
            [Authorize(Roles = "Admin")]
            public async Task<ActionResult<List<AllDto>>> GetAllCustomers()
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers.Select(EntityMappers.ToCustomerAllDto).ToList());
            }

            [HttpPost("{id}/createaccount")]
            [Authorize(Roles = "User,Admin")]
            public async Task<ActionResult> CreateAccount(string customerId, [FromBody] AccountCreateDto dto)
            {
                var account = await _accountService.CreateAccountAsync(dto, customerId);
                var accountDto = EntityMappers.MapAccountToDetailsDto(account);
                return Ok(accountDto);
            }

            [HttpPost]
            [Authorize(Roles = "User,Admin")]
            public async Task<ActionResult<AllDto>> CreateCustomer([FromBody] FormDto dto)
            {
                var customer = await _customerService.RegisterCustomer(dto);
                var customerDto = EntityMappers.ToCustomerAllDto(customer);
                return Ok(customerDto);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<AllDto>> UpdateCustomer(string id, [FromBody] UpdateDto dto)
            {
                var updateCustomerProfile = await _customerService.UpdateCustomerProfile(id, dto);
                return Ok(EntityMappers.ToCustomerAllDto(updateCustomerProfile));
            }

            [HttpDelete("{id}")]
            [Authorize(Roles = "User,Admin")]
            public async Task<ActionResult> DeleteCustomer(string id)
            {
                await _customerService.DeleteCustomer(id);
                return NoContent();
            }
        }
    }
}