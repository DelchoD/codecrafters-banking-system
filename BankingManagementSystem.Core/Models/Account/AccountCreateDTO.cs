namespace BankingManagementSystem.Core.Models.Account
{
    public class AccountCreateDto
    {
        public long AccountId { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public string Iban { get; set; } = string.Empty;

        public decimal Balance { get; set; }

        public long CustomerId { get; set; }
    }
}