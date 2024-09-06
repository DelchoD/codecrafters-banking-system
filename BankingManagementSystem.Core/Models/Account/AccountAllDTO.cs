using BankingManagementSystem.Core.Models.User;

namespace BankingManagementSystem.Core.Models.Account
{


    public class AccountAllDTO
    {
     
        public string IBAN { get; set; } = string.Empty;


        public string Name { get; set; } = string.Empty;


        public decimal Balance { get; set; }

    }
}
