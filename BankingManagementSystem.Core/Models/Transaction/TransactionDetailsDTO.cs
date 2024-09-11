

namespace BankingManagementSystem.Core.Models.Transaction
{
    using Account;

    public class TransactionDetailsDTO : TransactionAllDTO
    {
        public string Reason { get; set; } = string.Empty;

<<<<<<< HEAD
        public string? IbanFrom { get; set; }

        public string? IbanTo { get; set; } 

=======
>>>>>>> 8a7be6a (Implemented TransactionController and DTOs.)
        public string? CreatorId { get; set; }

        public string? Creator { get; set; }

    }
}
