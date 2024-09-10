using BankingManagementSystem.Infrastructure.Data.Models;
namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> ProcessTransaction(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
        Task<Transaction> GetTransactionById(int transactionId);

        Task<List<Transaction>> GetTransactionsByIBAN(string iban);
        Task<List<Transaction>> GetTransactionsByDate(string iban, DateTime startDate, DateTime endDate);
        Task<List<Transaction>> GetTransactionsByAmount(string iban, decimal minAmount, decimal maxAmount);
        Task<List<Transaction>> GetOutgoingTransactions(string iban);
        Task<List<Transaction>> GetIncomingTransactions(string iban);
        Task<List<Transaction>> GetTransactionsByStatus(string iban, string status);
        Task<Transaction> UpdateTransactionStatus(int transactionId, string newStatus);
        Task CancelTransaction(int transactionId);
    }
}
