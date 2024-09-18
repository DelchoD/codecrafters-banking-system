using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Core.Repositories;

namespace BankingManagementSystem.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly CustomerRepository _customerRepository;

  

        public AccountService(AccountRepository accountRepository, CustomerRepository customerRepository)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        public List<Account> GetCustomerAccounts(Customer customer)
        {
            return customer.Accounts.ToList();
        }

        public async Task<Account> GetAccountByIbanAsync(string iban)
        {
            if (iban is null)
                throw new ArgumentNullException(nameof(iban));

            return await _accountRepository.FirstOrDefaultAsync(a => a.Iban == iban)!;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.ToListAsync();
        }

        public async Task<Account> CreateAccountAsync(AccountCreateDto dto, string customerId)
        {
            var customer = await _customerRepository.FindAsync(customerId);
            if (customer is null)
                throw new KeyNotFoundException($"Customer with ID: {customerId} not found. Cannot create an account");

            var account = new Account
            {
                Iban = dto.Iban,
                Name = dto.Name,
                Balance = dto.Balance,
                CustomerId = customerId,
                Customer = customer,
                TransactionsFrom = new List<Transaction>(),
                TransactionsTo = new List<Transaction>()
            };

         

            await _accountRepository.AddAsync(account);
            await _accountRepository.SaveChangesAsync();
            return account;
        }

        public async Task<Account?> GetAccountByIdAsync(string accountId)
        {
            return await _accountRepository.FindAsync(accountId);
        }

        public async Task<Account> UpdateAccountBalance(string accountId, decimal newBalance)
        {
            var account = GetAccountByIdAsync(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            account.Balance = newBalance;
            //_accountRepository.Update(account);
            await _accountRepository.SaveChangesAsync();
            return account;
        }

        public async Task<bool> CloseAccountAsync(string accountId)
        {
            var account = GetAccountByIdAsync(accountId).Result;
            if (account is null)
                throw new KeyNotFoundException($"Account with ID {accountId} not found");

            _accountRepository.Remove(account);
            await _accountRepository.SaveChangesAsync();
            return true;
        }
    }
}