
using BankingManagementSystem.Core.Models.Account;

namespace BankingManagementSystem.Core.Models.Transaction
{
    public class TransactionAllDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public AccountTransactionDto IBANFrom { get; set; } = null!;

        public AccountTransactionDto IBANTo { get; set; } = null!;

    }
}