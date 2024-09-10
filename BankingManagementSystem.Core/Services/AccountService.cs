using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Infrastructure.Data.Models;

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

        public List<Account> GetCustomerAccounts(Customer customer)
        {
            return customer.Accounts.ToList();
        }

        public async Task<Account> GetAccountByIBAN(string iban)
        {
            if (iban is null)
                throw new ArgumentNullException(nameof(iban));
            return await _context.Accounts
            .FirstOrDefaultAsync(a => a.IBAN == iban);
        }

        public async Task<Account> CreateAccount(Account account, Customer customer)
        {
            if (customer is null)
                throw new KeyNotFoundException("Customer not found (is null). Cannot create an account");

            account.Customer = customer;
            await _context.Accounts.AddAsync(account);
            customer.Accounts.Add(account);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountById(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<Account> UpdateAccountBalance(int accountId, decimal newBalance)
        {
            var account = GetAccountById(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            account.Balance = newBalance;
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> CloseAccount(int accountId)
        {
            var account = GetAccountById(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}