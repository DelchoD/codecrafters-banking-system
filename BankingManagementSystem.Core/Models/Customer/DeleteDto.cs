namespace BankingManagementSystem.Core.Models.Customer
{
    public class DeleteDto
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string PersonalIdNumber { get; set; } = string.Empty;
    }
}