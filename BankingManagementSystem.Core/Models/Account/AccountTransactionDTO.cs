namespace BankingManagementSystem.Core.Models.Account
{
    public class AccountTransactionDto
    {
        public long TransactionId { get; set; }

        public string Iban { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}