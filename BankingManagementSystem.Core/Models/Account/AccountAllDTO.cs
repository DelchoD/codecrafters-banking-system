using BankingManagementSystem.Core.Models.User;

namespace BankingManagementSystem.Core.Models.Account
{

    public class AccountAllDTO : AccountTransactionDTO
    {
        
        public decimal Balance { get; set; }

    }
}
