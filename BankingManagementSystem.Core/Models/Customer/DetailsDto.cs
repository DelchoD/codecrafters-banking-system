namespace BankingManagementSystem.Core.Models.Customer
{
    using Account;

    public class DetailsDto : AllDto
    {
        public string PhoneNumber { get; set; } = string.Empty;

        public string DateOfBirth { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public new ICollection<AccountDetailsDto> Accounts { get; set; }
            = new List<AccountDetailsDto>();
    }
}