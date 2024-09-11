using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Data.Constants.ValidationConstants;


    public class Customer : IdentityUser
    {
        [Required]
        [MaxLength(CustomerFirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(CustomerMiddleNameMaxLength)]
        public string MiddleName { get; set; } = string.Empty;


        [Required]
        [MaxLength(CustomerLastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(CustomerIdNumberMaxLength)]
        public string PersonalIDNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public override string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(CustomerPasswordMinLength)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(CustomerAddressMaxLength)]
        public string Address { get; set; } = string.Empty;

        public ICollection<Account> Accounts { get; set; }
           = new List<Account>();

        public ICollection<Transaction> Transactions { get; set; }
           = new List<Transaction>();
    }
}
