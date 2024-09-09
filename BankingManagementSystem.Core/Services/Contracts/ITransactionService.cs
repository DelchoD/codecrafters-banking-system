using BankingManagementSystem.Infrastructure.Data.Models;
namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> ProcessTransaction(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
        Task<Transaction> GetTransactionById(int transactionId);
       
    }
}
