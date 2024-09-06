

namespace BankingManagementSystem.Core.Models.Transaction
{
    using Account;

    public class TransactionDetailsDTO : TransactionAllDTO
    {
        public string Reason { get; set; } = string.Empty;

        public AccountAllDTO? IBANFrom { get; set; }

        public AccountAllDTO? IBANTo { get; set; } 

        public string? CreatorId { get; set; }

        public string? Creator { get; set; }

    }
}
