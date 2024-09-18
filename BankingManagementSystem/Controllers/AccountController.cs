﻿using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystem.Controllers
{

    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountById(string id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account is null)
                return NotFound();

            var accountDetails = EntityMappers.MapAccountToDetailsDto(account);
            return Ok(accountDetails);
        }

        [HttpGet]
        public async Task<ActionResult<List<AccountDetailsDto>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDetails = accounts.Select(EntityMappers.MapAccountToDetailsDto).ToList();
            return Ok(accountDetails);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccount(string id)
        {
            //TODO: Add a check for the customer associated with the account
            await _accountService.CloseAccountAsync(id);
            return NoContent();
        }
    }
}