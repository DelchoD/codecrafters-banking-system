using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Core.Services;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountCreateDto>> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account is null)
                return NotFound();
            return Ok(account);
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccount()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDto dto)
        { 
            var account = await _accountService.CreateAccountAsync(dto);
            var accountDetailsDto = new AccountDetailsDto
            {
                Balance = account.Balance,
                CustomerId = int.Parse(account.CustomerId),
                Iban = account.IBAN,
                Name = account.Name,
                TransactionsFrom = new List<TransactionAllDTO>(),
                TransactionsTo = new List<TransactionAllDTO>()
            };
            return Ok(account);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            await _accountService.CloseAccountAsync(id);
            return NoContent();
        }
    }
}
