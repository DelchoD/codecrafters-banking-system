

namespace BankingManagementSystem.Core.Models.Transaction
{
    using Account;

    public class TransactionDetailsDTO : TransactionAllDTO
    {
        public string Reason { get; set; } = string.Empty;

        public string? CreatorId { get; set; }

        public string? Creator { get; set; }

    }
}
