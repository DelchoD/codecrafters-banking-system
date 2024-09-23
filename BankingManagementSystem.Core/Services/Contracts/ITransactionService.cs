using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();

        Task<Transaction> ProcessTransaction(TransactionCreateDto transactionCreateDto);

        List<Transaction> GetTransactionsByAccountId(string accountId);

        Task<Transaction> GetTransactionById(int transactionId);

        List<Transaction> GetTransactionsByDate(string accountId, DateTime startDate, DateTime endDate);

        List<Transaction> GetTransactionsByAmount(string accountId, decimal minAmount, decimal maxAmount);

        List<Transaction> GetOutgoingTransactions(string accountId);

        List<Transaction> GetIncomingTransactions(string accountId);

        Task<bool> CancelTransaction(int transactionId);
    }
}