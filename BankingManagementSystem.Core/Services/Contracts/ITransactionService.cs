namespace BankingManagementSystem.Core.Services.Contracts
{
    using BankingManagementSystem.Infrastructure.Data.Models;
    using BankingManagementSystem.Core.Models.Transaction;

    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> ProcessTransaction(TransactionCreateDTO transactionCreateDTO);
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
        Task<Transaction> GetTransactionById(int transactionId);
        Task<List<Transaction>> GetTransactionsByDate(int accountId, DateTime startDate, DateTime endDate);
        Task<List<Transaction>> GetTransactionsByAmount(int accountId, decimal minAmount, decimal maxAmount);
        Task<List<Transaction>> GetOutgoingTransactions(int accountId);
        Task<List<Transaction>> GetIncomingTransactions(int accountId);
        Task<bool> CancelTransaction(int transactionId);
    }
}

