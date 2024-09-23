using BankingManagementSystem.Core.Models.Account;
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
        public async Task<ActionResult<List<AccountDetailsDto>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDetails = accounts.Select(EntityMappers.MapAccountToDetailsDto).ToList();

            return Ok(accountDetails);
        }

        [HttpDelete("{accountId}/{customerId}")]
        public async Task<ActionResult> DeleteAccount(string accountId, string customerId)
        {
            await _accountService.CloseAccountAsync(accountId, customerId);
            return NoContent();
        }
    }
}