using System.Collections.Generic;
using System.Threading.Tasks;
using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    using Contracts;

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly AccountService _accountService;

        public TransactionService(ApplicationDbContext context, AccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            if (_context == null)
            {
                throw new InvalidOperationException("DbContext is not initialized.");
            }

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

            if (!int.TryParse(transaction.IBANFromId, out int ibanFromId))
                throw new ArgumentException($"Invalid IBAN format for source account: '{transaction.IBANFromId}'.");

            if (!int.TryParse(transaction.IBANToId, out int ibanToId))
                throw new ArgumentException($"Invalid IBAN format for destination account: '{transaction.IBANToId}'.");

            using var transactionDb = await _context.Database.BeginTransactionAsync();
            try
            {
                var accountFrom = await _accountService.GetAccountById(ibanFromId);
                var accountTo = await _accountService.GetAccountById(ibanToId);

                if (accountFrom == null)
                    throw new KeyNotFoundException($"Source account with ID '{ibanFromId}' was not found.");

                if (accountTo == null)
                    throw new KeyNotFoundException($"Destination account with ID '{ibanToId}' was not found.");

                if (accountFrom.Balance < transaction.TotalAmount)
                    throw new InvalidOperationException($"Insufficient funds in source account with ID '{ibanFromId}'.");

                accountFrom.Balance -= transaction.TotalAmount;
                accountTo.Balance += transaction.TotalAmount;

                _context.Transactions.Add(transaction);
                _context.Accounts.Update(accountFrom);
                _context.Accounts.Update(accountTo);

                await _context.SaveChangesAsync();
                await transactionDb.CommitAsync();

                return transaction;
            }
            catch (DbUpdateException dbEx)
            {
                await transactionDb.RollbackAsync();
                throw new InvalidOperationException("A database error occurred while processing the transaction.", dbEx);
            }
            catch (Exception ex)
            {
                await transactionDb.RollbackAsync();
                throw new InvalidOperationException("An unexpected error occurred while processing the transaction.", ex);
            }
        }


        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            if (accountId <= 0)
                throw new ArgumentException("Account ID must be a positive integer.", nameof(accountId));

            var accountIdString = accountId.ToString();

            return await _context.Transactions
                .Where(t => t.IBANFromId == accountIdString || t.IBANToId == accountIdString)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            if (transactionId <= 0)
                throw new ArgumentException("Transaction ID must be a positive integer.", nameof(transactionId));

            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            return transaction;
        }
    }
}