using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface IAccountService
    {
        Task<List<Account>> GetCustomerAccounts(string customerId);

        Task<List<Account>> GetAllAccountsAsync();

        Task<bool> CloseAccountAsync(string accountId, string customerId);

        Task<Account> UpdateAccountBalance(string accountId, decimal newBalance);

        Task<Account> GetAccountByIdAsync(string accountId);

        Task<Account> CreateAccountAsync(AccountCreateDto dto, string customerId);
      
        Task<Account> GetAccountByIbanAsync(string iban);
    }
}