﻿using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Core.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BankingManagementSystem.Infrastructure.Data.Models;
    using BankingManagementSystem.Core.Models.Transaction;

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAccountService _accountService;

        public TransactionService(ApplicationDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        // CREATE AND UPDATE

        public async Task<Transaction> ProcessTransaction(TransactionCreateDTO transactionCreateDTO)
        {
            if (transactionCreateDTO == null)
                throw new ArgumentNullException(nameof(transactionCreateDTO));

            var accountFrom = await _accountService.GetAccountByIbanAsync(transactionCreateDTO.IBANFrom.Iban);
            var accountTo = await _accountService.GetAccountByIbanAsync(transactionCreateDTO.IBANTo.Iban);

            if (accountFrom == null)
                throw new KeyNotFoundException($"Source account with IBAN '{transactionCreateDTO.IBANFrom.Iban}' was not found.");

            if (accountTo == null)
                throw new KeyNotFoundException($"Destination account with IBAN '{transactionCreateDTO.IBANTo.Iban}' was not found.");

            if (accountFrom.Balance < transactionCreateDTO.TotalAmount)
                throw new InvalidOperationException($"Insufficient funds in source account with IBAN '{transactionCreateDTO.IBANFrom.Iban}'.");

            var transaction = new Transaction
            {
                Date = transactionCreateDTO.Date,
                TotalAmount = transactionCreateDTO.TotalAmount,
                Reason = transactionCreateDTO.Reason,
                IBANFromId = transactionCreateDTO.IBANFrom.Iban,
                IBANToId = transactionCreateDTO.IBANTo.Iban
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

        // READ

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

        public async Task<List<Transaction>> GetTransactionsByAccountId(string accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            return account.TransactionsFrom.ToList();
        }

        public async Task<TransactionDetailsDTO> GetTransactionById(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            var transactionDTO = new TransactionDetailsDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                Reason = transaction.Reason,
                IBANFrom = new AccountTransactionDTO { IBAN = transaction.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = transaction.IBANToId }
            };

            return transactionDTO;
        }

        public async Task<List<TransactionDetailsDTO>> GetTransactionsByDate(int accountId, DateTime startDate, DateTime endDate)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            var filteredTransactions = account.TransactionsFrom
             .Where(t => t.Date >= startDate && t.Date <= endDate)
             .OrderByDescending(t => t.Date)
             .ToList();


            var transactionDTOs = filteredTransactions.Select(t => new TransactionDetailsDTO
            {
                Id = t.Id,
                Date = t.Date,
                TotalAmount = t.TotalAmount,
                Reason = t.Reason,
                IBANFrom = new AccountTransactionDTO { IBAN = t.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = t.IBANToId }
            }).ToList();

            return transactionDTOs;
        }

        public async Task<List<TransactionDetailsDTO>> GetTransactionsByAmount(int accountId, decimal minAmount, decimal maxAmount)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");
            
            var filteredTransactions = account.TransactionsFrom
                    .Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount)
                    .Concat(account.TransactionsTo
                        .Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount))
                    .OrderByDescending(t => t.TotalAmount)
                    .ToList();

            var transactionDTOs = filteredTransactions.Select(t => new TransactionDetailsDTO
            {
                Id = t.Id,
                Date = t.Date,
                TotalAmount = t.TotalAmount,
                Reason = t.Reason,
                IBANFrom = new AccountTransactionDTO { IBAN = t.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = t.IBANToId }
            }).ToList();

            return transactionDTOs;

        }

        public async Task<List<TransactionDetailsDTO>> GetOutgoingTransactions(int accountId)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");
            var outgoingTransactions = account.TransactionsFrom.ToList();
            var transactionDTOs = outgoingTransactions.Select(t => new TransactionDetailsDTO
            {
                Id = t.Id,
                Date = t.Date,
                TotalAmount = t.TotalAmount,
                Reason = t.Reason,
                IBANFrom = new AccountTransactionDTO { IBAN = t.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = t.IBANToId }
            }).ToList();

            return transactionDTOs;
        }

        public async Task<List<TransactionDetailsDTO>> GetIncomingTransactions(int accountId)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");
            var incomingTransactions = account.TransactionsTo.ToList();
            var transactionDTOs = incomingTransactions.Select(t => new TransactionDetailsDTO
            {
                Id = t.Id,
                Date = t.Date,
                TotalAmount = t.TotalAmount,
                Reason = t.Reason,
                IBANFrom = new AccountTransactionDTO { IBAN = t.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = t.IBANToId }
            }).ToList();

            return transactionDTOs;
        }

        /*DELETE (according to the Internet, a transaction can only be deleted if it is invalid or fraudulent,
         * but in such a case, the funds are returned to the account that sent them)*/
        public async Task<bool> CancelTransaction(int transactionId)
        {
            var transaction = await _context.Transactions
                .FindAsync(transactionId);

            if (transaction == null)
                return false;

            var accountFrom = await _accountService.GetAccountByIBAN(transaction.IBANFromId);
            var accountTo = await _accountService.GetAccountByIBAN(transaction.IBANToId);

            if (accountFrom != null)
                accountFrom.Balance += transaction.TotalAmount;
            if (accountTo != null)
                accountTo.Balance -= transaction.TotalAmount;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Transaction>> GetTransactionsByDate(string accountId, DateTime startDate, DateTime endDate)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            var filteredTransactions = account.TransactionsFrom
                .Where(t => t.Date >= startDate && t.Date <= endDate)
                .OrderByDescending(t => t.Date)
                .ToList();

            return filteredTransactions;
        }

        public async Task<List<Transaction>> GetTransactionsByAmount(string accountId, decimal minAmount, decimal maxAmount)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            var filteredTransactions = account.TransactionsFrom
                .Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount)
                .Concat(account.TransactionsTo
                    .Where(t => t.TotalAmount >= minAmount && t.TotalAmount <= maxAmount))
                .OrderByDescending(t => t.TotalAmount)
                .ToList();

            return filteredTransactions;
        }

        public async Task<List<Transaction>> GetOutgoingTransactions(string accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            var outgoingTransactions = account.TransactionsFrom.ToList();

            return outgoingTransactions;
        }

        public async Task<List<Transaction>> GetIncomingTransactions(string accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with ID '{accountId}' was not found.");

            return account.TransactionsTo.ToList();
        }

        /*DELETE (according to the Internet, a transaction can only be deleted if it is invalid or fraudulent,
         * but in such a case, the funds are returned to the account that sent them)*/
        public async Task<bool> CancelTransaction(int transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);

            if (transaction == null)
                return false;

            var accountFrom = await _accountService.GetAccountByIbanAsync(transaction.IBANFromId);
            var accountTo = await _accountService.GetAccountByIbanAsync(transaction.IBANToId);

            if (accountFrom != null)
                accountFrom.Balance += transaction.TotalAmount;
            if (accountTo != null)
                accountTo.Balance -= transaction.TotalAmount;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
