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

        public async Task<Account> CreateAccount(Customer customer)
        {
            // if this function receives only the customer's ID, uncomment line below:
            //var customer = await _customerService.GetCustomerById(customerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found. Cannot create an account");

            var account = new Account
            {
                IBAN = "BG" + new Random().Next(100000000, 999999999),
                Name = customer.FirstName + "'s " + "account",
                Balance = 0m,
                CustomerId = customer.Id
            };
            
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
