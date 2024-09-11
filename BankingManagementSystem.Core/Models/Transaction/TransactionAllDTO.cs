using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Core.Models.Account;

namespace BankingManagementSystem.Core.Models.Transaction
{
    public class TransactionAllDTO
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public AccountTransactionDTO IBANFrom { get; set; } = null!;

        public AccountTransactionDTO IBANTo { get; set; } = null!;

    }
}