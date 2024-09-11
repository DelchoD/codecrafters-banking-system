using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Services;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Controllers
{
    using BankingManagementSystem.Core.Models.Transaction;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {

        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountById(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account is null)
                return NotFound();

            var accountDetails = MapAccountToDetailsDto(account);
            return Ok(accountDetails);
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            var accountDetails = accounts.Select(MapAccountToDetailsDto).ToList();
            return Ok(accountDetails);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            await _accountService.CloseAccountAsync(id);
            return NoContent();
        }

        private AccountDetailsDto MapAccountToDetailsDto(Account account)
        {
            return new AccountDetailsDto
            {
                AccountId = account.Id,
                Name = account.Name,
                Iban = account.Iban,
                Balance = account.Balance,
                CustomerId = account.CustomerId,
                TransactionsFrom = account.TransactionsFrom.Select(MapTransactionToAllDto).ToList(),
                TransactionsTo = account.TransactionsTo.Select(MapTransactionToAllDto).ToList()
            };
        }

        private TransactionDetailsDTO MapTransactionToAllDto(Transaction transaction)
        {
            return new TransactionDetailsDTO
            {
                Id = transaction.Id,
                TotalAmount = transaction.TotalAmount,
                Date = transaction.Date,
                IbanFrom = transaction.IBANFrom.Iban,
                IbanTo = transaction.IBANTo.Iban
            };
        }
    }
}