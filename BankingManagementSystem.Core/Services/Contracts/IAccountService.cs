using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface IAccountService
    {
        Task<List<Account>> GetCustomerAccounts(Customer customer);
        Task<Account> CloseAccount(int accountId);
        Task<bool> UpdateAccountBalance(int accountId, decimal newBalance);
        Task<Account?> GetAccountById(int accountId);
        Task<Account> CreateAccount(Account account, Customer customer);
    }
}
