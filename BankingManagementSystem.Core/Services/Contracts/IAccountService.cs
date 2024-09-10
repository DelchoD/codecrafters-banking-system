using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface IAccountService
    {
        List<Account> GetCustomerAccounts(Customer customer);
        Task<bool> CloseAccount(int accountId);
        Task<Account> UpdateAccountBalance(int accountId, decimal newBalance);
        Task<Account?> GetAccountById(int accountId);
        Task<Account> CreateAccount(Account account, Customer customer);
        Task<Account> GetAccountByIBAN(string iban);
    }
}
