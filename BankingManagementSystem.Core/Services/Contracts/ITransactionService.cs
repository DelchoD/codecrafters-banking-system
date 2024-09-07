using System.Collections.Generic;
using System.Threading.Tasks;
using BankingManagementSystem.Infrastructure.Data.Models;
namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> ProcessTransaction(Transaction transaction);
        Task<List<Transaction>> GetTransactionsByAccountId(string accountId);
        Task<Transaction> GetTransactionById(string transactionId);
    }
}
