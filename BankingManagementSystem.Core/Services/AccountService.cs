using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Account> GetCustomerAccounts(Customer customer)
        {
            return customer.Accounts.ToList();
        }

        public async Task<Account> GetAccountByIbanAsync(string iban)
        {
            if (iban is null)
                throw new ArgumentNullException(nameof(iban));

            return (await _context.Accounts.FirstOrDefaultAsync(a => a.Iban == iban))!;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> CreateAccountAsync(AccountCreateDto dto, string customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer is null)
                throw new KeyNotFoundException($"Customer with ID: {customerId} not found. Cannot create an account");

            var account = new Account
            {
                Iban = dto.Iban,
                Name = dto.Name,
                Balance = dto.Balance,
                Customer = customer,
                TransactionsFrom = new List<Transaction>(),
                TransactionsTo = new List<Transaction>()
            };

            await _context.Accounts.AddAsync(account);
            customer.Accounts.Add(account);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetAccountByIdAsync(string accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            return account;
        }

        public async Task<Account> UpdateAccountBalance(string accountId, decimal newBalance)
        {
            var account = GetAccountByIdAsync(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            account.Balance = newBalance;
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> CloseAccountAsync(string accountId, string customerId)
        {
            var account = GetAccountByIdAsync(accountId).Result;
            if (account.CustomerId != customerId)
                throw new InvalidOperationException($"Customer with id:{customerId} is not owner of account with id:{account}");

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}