namespace BankingManagementSystem.Core.Models.Customer
{
    using Account;
    using Transaction;
    using User;
    public class CustomerDetailsDTO : CustomerAllDTO
    {

        public string PhoneNumber { get; set; } = string.Empty;

        public string DateOfBirth { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

       public ICollection<AccountAllDto> Accounts { get; set; }
           = new List<AccountAllDto>();

        public ICollection<TransactionAllDTO> Transactions { get; set; }
           = new List<TransactionAllDTO>();
    }
}
