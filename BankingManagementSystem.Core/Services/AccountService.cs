using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Account>> GetCustomerAccounts(Customer customer)
        {
            return _context.Accounts.AsNoTracking().Where(a => a.CustomerId == customer.Id).ToListAsync();
        }

        public async Task<Account> CreateAccount(Account account, Customer customer)
        {
            if (customer is null)
                throw new KeyNotFoundException("Customer not found (is null). Cannot create an account");
            
            await _context.Accounts.AddAsync(account);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountById(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<bool> UpdateAccountBalance(int accountId, decimal newBalance)
        {
            var account = GetAccountById(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            account.Balance = newBalance;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> CloseAccount(int accountId)
        {
            var account = GetAccountById(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}
