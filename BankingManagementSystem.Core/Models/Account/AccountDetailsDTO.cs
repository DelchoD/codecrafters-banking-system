namespace BankingManagementSystem.Core.Models.Account
{
    using Transaction;

    public class AccountDetailsDto
    {
        public string? CreatorId { get; set; }

        public string? Creator { get; set; }

        public string AccountId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Iban { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public string CustomerId { get; set; } = string.Empty;

        public ICollection<TransactionDetailsDto> TransactionsFrom { get; set; } 
            = new List<TransactionDetailsDto>();

        public ICollection<TransactionDetailsDto> TransactionsTo { get; set; } 
            = new List<TransactionDetailsDto>();
    }
}