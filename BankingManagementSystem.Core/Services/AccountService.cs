using BankingManagementSystem.Core.Models.Account;
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

        public List<Account> GetCustomerAccounts(Customer customer)
        {
            return customer.Accounts.ToList();
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }   

        public async Task<Account> CreateAccountAsync(AccountCreateDto dto)
        {
            var customer = await _context.Customers.FindAsync(dto.CustomerId);
            if (customer is null)
                throw new KeyNotFoundException("Customer not found (is null). Cannot create an account");

            var account = new Account
            {
                Iban = dto.Iban,
                Name = dto.Name,
                Balance = dto.Balance,
                CustomerId = dto.CustomerId,
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

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public async Task<Account> UpdateAccountBalance(int accountId, decimal newBalance)
        {
            var account = GetAccountByIdAsync(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            account.Balance = newBalance;
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<bool> CloseAccountAsync(int accountId)
        {
            var account = GetAccountByIdAsync(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}