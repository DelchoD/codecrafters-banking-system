using BankingManagementSystem.Core.Models.Account;

namespace BankingManagementSystem.Core.Models.User
{

    public class CustomerAllDTO
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PersonalIDNumber { get; set; } = string.Empty;

        public ICollection<AccountDetailsDto> Accounts { get; set; }
         = new List<AccountDetailsDto>();
    }
}
