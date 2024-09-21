using BankingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransactionService(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<Transaction> ProcessTransaction(TransactionCreateDto transactionCreateDto)
        {
            if (transactionCreateDto == null)
                throw new ArgumentNullException(nameof(transactionCreateDto));

            var accountFrom = await _accountService.GetAccountByIbanAsync(transactionCreateDto.IbanFrom);
            var accountTo = await _accountService.GetAccountByIbanAsync(transactionCreateDto.IbanTo);

            if (accountFrom.Balance < transactionCreateDto.TotalAmount)
                throw new InvalidOperationException($"Insufficient funds in source account with IBAN '{transactionCreateDto.IbanTo}'.");

            var transaction = new Transaction
            {
                Date = transactionCreateDto.Date,
                TotalAmount = transactionCreateDto.TotalAmount,
                Reason = transactionCreateDto.Reason,
                IbanFrom = transactionCreateDto.IbanFrom,
                IbanTo = transactionCreateDto.IbanTo,
            };

            accountFrom.Balance -= transaction.TotalAmount;
            accountTo.Balance += transaction.TotalAmount;

            accountFrom.TransactionsFrom.Add(transaction);
            accountTo.TransactionsTo.Add(transaction);

            _context.Transactions.Add(transaction);
            _context.Accounts.Update(accountFrom);
            _context.Accounts.Update(accountTo);

            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            try
            {
                return await _context.Transactions.ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new InvalidOperationException("A database error occurred while retrieving transactions.", dbEx);
            }
        }

        public List<Transaction> GetTransactionsByAccountId(string accountId)
        {
            var account = GetAccountWithTransactionsById(accountId).Result;
            var transactions = account.TransactionsFrom.Concat(account.TransactionsTo).ToList();

            return transactions;
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            return transaction;
        }

        public List<Transaction> GetTransactionsByDate(string accountId, DateTime startDate, DateTime endDate)
        {
            var account = GetAccountWithTransactionsById(accountId).Result;

            var filteredTransactions = account.TransactionsFrom
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .Concat(account.TransactionsTo)
                    .Where(t => t.Date >= startDate && t.Date <= endDate)
                .OrderByDescending(t => t.Date)
                .ToList();

            return filteredTransactions;
        }

        public List<Transaction> GetTransactionsByAmount(string accountId, decimal minAmount, decimal maxAmount)
        {
            var account = GetAccountWithTransactionsById(accountId).Result;

            var filteredTransactions = account.TransactionsFrom
                .Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount)
                .Concat(account.TransactionsTo
                    .Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount))
                .OrderByDescending(t => t.TotalAmount)
                .ToList();

            return filteredTransactions;
        }

        public List<Transaction> GetOutgoingTransactions(string accountId)
        {
            var account = GetAccountWithTransactionsById(accountId).Result;

            return account.TransactionsFrom.ToList();
        }

        public List<Transaction> GetIncomingTransactions(string accountId)
        {
            var account = GetAccountWithTransactionsById(accountId).Result;

            return account.TransactionsTo.ToList();
        }

        /*
         * according to the Internet, a transaction can only be deleted if it is invalid or fraudulent,
         * but in such a case, the funds are returned to the account that sent them
         */
        public async Task<bool> CancelTransaction(int transactionId)
        {
            var transaction = await _context.Transactions
                .Where(t => t.Id == transactionId)
                .FirstOrDefaultAsync();

            if (transaction == null)
                return false;

            var accountFrom = await _accountService.GetAccountByIbanAsync(transaction.IbanFrom);
            var accountTo = await _accountService.GetAccountByIbanAsync(transaction.IbanTo);

            accountFrom.Balance += transaction.TotalAmount;
            accountTo.Balance -= transaction.TotalAmount;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<Account> GetAccountWithTransactionsById(string accountId)
        {
            var account = await _context.Accounts
                .Where(a => a.Id == accountId)
                .Include(a => a.TransactionsFrom)
                .Include(a => a.TransactionsTo)
                .FirstOrDefaultAsync();

            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            return account;
        }
    }
}