using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;

    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerService _customerService;

        public AccountService(ApplicationDbContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        public List<Account> GetCustomerAccounts(Customer customer)
        {
            return _context.Accounts.Where(a => a.CustomerId == customer.Id).ToList();
        }

        public async Task<Account> CreateAccount(Account account, Customer customer)
        {
            if (customer is null)
                throw new KeyNotFoundException("Customer not found (is null). Cannot create an account");
            
            _context.Accounts.Add(account);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountById(int accountId)
        {
            return await _context.Accounts.FindAsync(accountId);
        }

        public bool UpdateAccountBalance(int accountId, decimal newBalance)
        {
            var account = GetAccountById(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            account.Balance = newBalance;
            _context.Accounts.Update(account);
            _context.SaveChangesAsync();
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
