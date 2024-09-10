
namespace BankingManagementSystem.Core.Models.Account
{
    using Transaction;

    public class AccountDetailsDto : AccountCreateDto
    {
        public string? CreatorId { get; set; }

        public string? Creator { get; set; }


        public ICollection<TransactionAllDTO> TransactionsFrom { get; set; }
         = new List<TransactionAllDTO>();


        public ICollection<TransactionAllDTO> TransactionsTo { get; set; }
          = new List<TransactionAllDTO>();
    }
}