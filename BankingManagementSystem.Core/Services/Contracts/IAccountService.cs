using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface IAccountService
    {
        List<Account> GetCustomerAccounts(Customer customer);

        Task<List<Account>> GetAllAccountsAsync();

        Task<bool> CloseAccountAsync(int accountId);

        Task<Account> UpdateAccountBalance(int accountId, decimal newBalance);

        Task<Account?> GetAccountByIdAsync(int accountId);

        Task<Account> CreateAccountAsync(AccountCreateDto dto);
    }
}