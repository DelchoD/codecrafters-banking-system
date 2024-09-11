namespace BankingManagementSystem.Core.Models.Account
{
    public class AccountDeleteDto
    {
        public long AccountId { get; set; }

        public string Iban { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}