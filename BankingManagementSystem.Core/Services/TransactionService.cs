using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    using Contracts;

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

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if(account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");
            return account.TransactionsFrom;

        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            return transaction;
        }
    }
}