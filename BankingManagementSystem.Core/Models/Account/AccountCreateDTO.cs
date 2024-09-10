namespace BankingManagementSystem.Core.Models.Account
{

    public class AccountCreateDto : AccountTransactionDto
    {
        public decimal Balance { get; set; }

        public int CustomerId { get; set; }
    }
}