namespace BankingManagementSystem.Core.Models.Customer
{
    using User;
    public class CustomerDetailsDTO : CustomerAllDTO
    {

        public string PhoneNumber { get; set; } = string.Empty;

        public string DateOfBirth { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }
}
