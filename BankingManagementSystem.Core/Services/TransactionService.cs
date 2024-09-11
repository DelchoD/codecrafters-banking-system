using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using Microsoft.EntityFrameworkCore.Update;

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransactionService(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
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

        //CREATE

        public async Task<Transaction> ProcessTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));


            var accountFrom = await _accountService.GetAccountByIbanAsync(transaction.IBANFromId);
            var accountTo = await _accountService.GetAccountByIbanAsync(transaction.IBANToId);

            if (accountFrom == null)
                throw new KeyNotFoundException($"Source account with IBAN '{transaction.IBANFromId}' was not found.");

            if (accountTo == null)
                throw new KeyNotFoundException($"Destination account with IBAN '{transaction.IBANToId}' was not found.");

            if (accountFrom.Balance < transaction.TotalAmount)
                throw new InvalidOperationException($"Insufficient funds in source account with IBAN '{transaction.IBANFromId}'.");

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

        //READ

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");
            return account.TransactionsFrom;
        }

        public async Task<Transaction> GetTransactionById(string transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            return transaction;
        }

        public async Task<List<Transaction>> GetTransactionsByIBAN(string iban)
        {
            var account = await _accountService.GetAccountByIBAN(iban);
            if (account == null)
                throw new KeyNotFoundException($"Account with IBAN '{iban}' was not found.");

            var transactions = account.TransactionsFrom.Concat(account.TransactionsTo).ToList();
            return transactions;
        }

        public async Task<List<Transaction>> GetTransactionsByDate(string iban, DateTime startDate, DateTime endDate)
        {
            var transactions = await GetTransactionsByIBAN(iban);

            var filteredTransactions = transactions.Where(t => t.Date >= startDate && t.Date <= endDate)
                .OrderByDescending(t => t.Date).ToList();

            return filteredTransactions;
        }

        public async Task<List<Transaction>> GetTransactionsByAmount(string iban, decimal minAmount, decimal maxAmount)
        {
            var transactions = await GetTransactionsByIBAN(iban);
            var filteredTransactions = transactions.Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount)
                .OrderByDescending(t => t.TotalAmount).ToList();

            return filteredTransactions;
        }

        public async Task<List<Transaction>> GetOutgoingTransactions(string iban)
        {
            var account = await _accountService.GetAccountByIBAN(iban);
            if (account == null)
                throw new KeyNotFoundException($"Account with IBAN '{iban}' was not found.");
            var outgoingTransactions = account.TransactionsFrom.ToList();
                return outgoingTransactions;
        }

        public async Task<List<Transaction>> GetIncomingTransactions(string iban)
        {
            var account = await _accountService.GetAccountByIBAN(iban);
            if (account == null)
                throw new KeyNotFoundException($"Account with IBAN '{iban}' was not found.");
            var incomingTransactions = account.TransactionsTo.ToList();
                return incomingTransactions;
        }

        public async Task<List<Transaction>> GetTransactionsByStatus(string iban, string status)
        {
            var transactions = await GetTransactionsByIBAN(iban);
            var filteredTransactions = transactions.Where(t => t.Status == status).ToList();

            return filteredTransactions;
        }


        //DELETE
        public async Task CancelTransaction(int transactionId)
        {
            var transaction = await GetTransactionById(transactionId);
                if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");
            var accountFrom = await _accountService.GetAccountByIBAN(transaction.IBANFromId);
            var accountTo = await _accountService.GetAccountByIBAN(transaction.IBANToId);
            if(accountFrom != null)
                accountFrom.Balance += transaction.TotalAmount;
            if(accountTo != null)
                accountTo.Balance -= transaction.TotalAmount;   
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

    }

}