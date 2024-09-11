namespace BankingManagementSystem.Core.Services.Contracts
{
    using BankingManagementSystem.Infrastructure.Data.Models;
    using BankingManagementSystem.Core.Models.Transaction;


    public interface ITransactionService
    {
        Task<List<TransactionDetailsDTO>> GetAllTransactionsAsync();
        Task<TransactionDetailsDTO> ProcessTransaction(TransactionCreateDTO transactionCreateDTO);
        Task<List<TransactionDetailsDTO>> GetTransactionsByAccountId(int accountId);
        Task<TransactionDetailsDTO> GetTransactionById(int transactionId);
        Task<List<TransactionDetailsDTO>> GetTransactionsByDate(int accountId, DateTime startDate, DateTime endDate);
        Task<List<TransactionDetailsDTO>> GetTransactionsByAmount(int accountId, decimal minAmount, decimal maxAmount);
        Task<List<TransactionDetailsDTO>> GetOutgoingTransactions(int accountId);
        Task<List<TransactionDetailsDTO>> GetIncomingTransactions(int accountId);
        Task<bool> CancelTransaction(int transactionId);
    }
}
