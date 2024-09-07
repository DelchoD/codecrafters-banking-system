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
            using var transactionDb = await _context.Database.BeginTransactionAsync();
            try
            {
                if (!int.TryParse(transaction.IBANFromId, out int ibanFromId))
                    throw new ArgumentException($"Invalid IBAN format for source account: '{transaction.IBANFromId}'.");

                if (!int.TryParse(transaction.IBANToId, out int ibanToId))
                    throw new ArgumentException($"Invalid IBAN format for destination account: '{transaction.IBANToId}'.");


                var accountFrom = await _accountService.GetAccountById(ibanFromId);
                var accountTo = await _accountService.GetAccountById(ibanToId);

                if (accountFrom == null)
                    throw new KeyNotFoundException($"Source account with ID '{ibanFromId}' was not found.");

                if (accountTo == null)
                    throw new KeyNotFoundException($"Destination account with ID '{ibanToId}' was not found.");

                if (accountFrom.Balance < transaction.TotalAmount)
                    throw new InvalidOperationException($"Insufficient funds in source account with ID '{accountFrom.IBAN}'.");

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
            catch (KeyNotFoundException keyNotFoundEx)
            {
                throw new KeyNotFoundException("An error occurred while retrieving the account.", keyNotFoundEx);
            }
            catch (InvalidOperationException invalidOpEx)
            {
                throw new InvalidOperationException("An error occurred while processing the transaction.", invalidOpEx);
            }
        }

        public async Task<List<Transaction>> GetTransactionsByAccountId(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
                throw new ArgumentException("Account ID cannot be null or empty.", nameof(accountId));

            return await _context.Transactions
                                .Where(t => t.IBANFromId == accountId || t.IBANToId == accountId)
                                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(string transactionId)
        {
            if (string.IsNullOrWhiteSpace(transactionId))
                throw new ArgumentException("Transaction ID cannot be null or empty.", nameof(transactionId));

            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            return transaction;
        }
    }
}