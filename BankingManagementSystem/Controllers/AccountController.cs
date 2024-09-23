using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Utils;
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
            var accountDetails = EntityMappers.MapAccountToDetailsDto(account);

            return Ok(accountDetails);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountByCustomerId(string customerId)
        {
            var accounts = await _accountService.GetCustomerAccounts(customerId);
            var accountDetails = accounts.Select(EntityMappers.MapAccountToDetailsDto);

            return Ok(accountDetails);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<AccountDetailsDto>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDetails = accounts.Select(EntityMappers.MapAccountToDetailsDto).ToList();
            if (accountDetails.Count == 0)
                return NotFound("No accounts found.");

            return Ok(accountDetails);
        }

        [HttpDelete("{accountId}/{customerId}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> DeleteAccount(string accountId, string customerId)
        {
            await _accountService.CloseAccountAsync(accountId, customerId);
            return NoContent();
        }
    }
}