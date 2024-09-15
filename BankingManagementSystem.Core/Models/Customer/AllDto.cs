using BankingManagementSystem.Core.Models.Account;

namespace BankingManagementSystem.Core.Models.Customer
{

    public class AllDto
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PersonalIdNumber { get; set; } = string.Empty;

        public ICollection<AccountDetailsDto> Accounts { get; set; }
         = new List<AccountDetailsDto>();
    }
}
