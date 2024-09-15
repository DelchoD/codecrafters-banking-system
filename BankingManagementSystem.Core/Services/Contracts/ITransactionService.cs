namespace BankingManagementSystem.Core.Services.Contracts
{
    using BankingManagementSystem.Infrastructure.Data.Models;
    using BankingManagementSystem.Core.Models.Transaction;

    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> ProcessTransaction(TransactionCreateDTO transactionCreateDTO);
        Task<List<Transaction>> GetTransactionsByAccountId(string accountId);
        Task<Transaction> GetTransactionById(int transactionId);
        Task<List<Transaction>> GetTransactionsByDate(string accountId, DateTime startDate, DateTime endDate);
        Task<List<Transaction>> GetTransactionsByAmount(string accountId, decimal minAmount, decimal maxAmount);
        Task<List<Transaction>> GetOutgoingTransactions(string accountId);
        Task<List<Transaction>> GetIncomingTransactions(string accountId);
        Task<bool> CancelTransaction(int transactionId);
    }
}

