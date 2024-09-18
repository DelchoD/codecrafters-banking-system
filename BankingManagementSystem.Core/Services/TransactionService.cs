using BankingManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data.Models;
using BankingManagementSystem.Core.Repositories;

namespace BankingManagementSystem.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IAccountService _accountService;
        private readonly AccountRepository _accountRepository;

        public TransactionService(TransactionRepository transactionRepository, IAccountService accountService, AccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountService = accountService;
            _accountRepository = accountRepository;
        }

        public async Task<Transaction> ProcessTransaction(TransactionCreateDto transactionCreateDto)
        {
            if (transactionCreateDto == null)
                throw new ArgumentNullException(nameof(transactionCreateDto));

            var accountFrom = await _accountService.GetAccountByIbanAsync(transactionCreateDto.IBANFrom.Iban);
            var accountTo = await _accountService.GetAccountByIbanAsync(transactionCreateDto.IBANTo.Iban);

            if (accountFrom == null)
                throw new KeyNotFoundException(
                    $"Source account with IBAN '{transactionCreateDto.IBANFrom.Iban}' was not found.");

            if (accountTo == null)
                throw new KeyNotFoundException(
                    $"Destination account with IBAN '{transactionCreateDto.IBANTo.Iban}' was not found.");

            if (accountFrom.Balance < transactionCreateDto.TotalAmount)
                throw new InvalidOperationException(
                    $"Insufficient funds in source account with IBAN '{transactionCreateDto.IBANFrom.Iban}'.");

            var transaction = new Transaction
            {
                Date = transactionCreateDto.Date,
                TotalAmount = transactionCreateDto.TotalAmount,
                Reason = transactionCreateDto.Reason,
                IBANFromId = transactionCreateDto.IBANFrom.Iban,
                IBANToId = transactionCreateDto.IBANTo.Iban
            };

            accountFrom.Balance -= transaction.TotalAmount;
            accountTo.Balance += transaction.TotalAmount;

            accountFrom.TransactionsFrom.Add(transaction);
            accountTo.TransactionsTo.Add(transaction);

           

            await _transactionRepository.AddAsync(transaction);
            _accountRepository.Update(accountFrom);
            _accountRepository.Update(accountTo);
            await _transactionRepository.SaveChangesAsync();

            return transaction;
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            try
            {
                return await _transactionRepository.ToListAsync();
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

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            var transaction = await _transactionRepository.FindAsync(transactionId);
            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with ID '{transactionId}' was not found.");

            return transaction;
        }

        public async Task<List<Transaction>> GetTransactionsByDate(string accountId, DateTime startDate,
            DateTime endDate)
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

        public async Task<List<Transaction>> GetTransactionsByAmount(string accountId, decimal minAmount,
            decimal maxAmount)
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

        /*
         * according to the Internet, a transaction can only be deleted if it is invalid or fraudulent,
         * but in such a case, the funds are returned to the account that sent them
         */
        public async Task<bool> CancelTransaction(int transactionId)
        {
            var transaction = await _transactionRepository.FindAsync(transactionId);

            if (transaction == null)
                return false;

            var accountFrom = await _accountService.GetAccountByIbanAsync(transaction.IBANFromId);
            var accountTo = await _accountService.GetAccountByIbanAsync(transaction.IBANToId);

            if (accountFrom != null)
                accountFrom.Balance += transaction.TotalAmount;
            if (accountTo != null)
                accountTo.Balance -= transaction.TotalAmount;

            _transactionRepository.Remove(transaction);
            await _transactionRepository.SaveChangesAsync();

            return true;
        }
    }
}