using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Core.Models.Account;

namespace BankingManagementSystem.Core.Models.Transaction
{
    public class TransactionAllDTO
    {
<<<<<<< HEAD
        public string Id { get; set; } = string.Empty;


        public DateTime Date { get; set; }
=======
        public int Id { get; set; }
>>>>>>> 8a7be6a (Implemented TransactionController and DTOs.)

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public AccountTransactionDTO IBANFrom { get; set; } = null!;

        public AccountTransactionDTO IBANTo { get; set; } = null!;

    }
}