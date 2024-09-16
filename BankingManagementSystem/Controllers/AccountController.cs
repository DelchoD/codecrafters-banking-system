using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Services;
using BankingManagementSystem.Infrastructure.Data.Models;
using BankingManagementSystem.Core.Models.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingManagementSystem.Controllers
{

    [ApiController]
    [Route("api/accounts")]
    [Authorize(Roles = "User,Admin")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountById(string id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account is null)
                return NotFound("Account not found.");

            var accountDetails = EntityMappers.MapAccountToDetailsDto(account);
            return Ok(accountDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AccountDetailsDto>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDetails = accounts.Select(MapAccountToDetailsDto).ToList();
            if (accountDetails.Count == 0)
                return NotFound("No accounts found.");

            return Ok(accountDetails);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> DeleteAccount(string id)
        {
            var response = await GetAccountById(id);
            if (response.Result is NotFoundResult)
                return response.Result;

            await _accountService.CloseAccountAsync(id);
            return NoContent();
        }
    }
}