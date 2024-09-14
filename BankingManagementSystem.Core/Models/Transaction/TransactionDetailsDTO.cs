

namespace BankingManagementSystem.Core.Models.Transaction
{
    using Account;

    public class TransactionDetailsDTO : TransactionAllDTO
    {
        public string Reason { get; set; } = string.Empty;


        public string? IbanFrom { get; set; }

        public string? IbanTo { get; set; } 

        public string? CreatorId { get; set; }

        public string? Creator { get; set; }

    }
}
