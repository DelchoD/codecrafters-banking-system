
namespace BankingManagementSystem.Core.Models.Account
{
    using Transaction;

    public class AccountDetailsDTO : AccountAllDTO
    {

        public string? Creator { get; set; }


        public ICollection<TransactionAllDTO> TransactionsFrom { get; set; }
         = new List<TransactionAllDTO>();


        public ICollection<TransactionAllDTO> TransactionsTo { get; set; }
          = new List<TransactionAllDTO>();
    }
}
