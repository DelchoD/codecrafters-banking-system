using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;


    public class Customer : IdentityUser
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(MiddleNameMaxLength)]
        public string MiddleName { get; set; } = string.Empty;


        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(IdNumberMaxLength)]
        public string PersonalIdNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public override string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(PasswordMinLength)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = string.Empty;

        public ICollection<Account> Accounts { get; set; }
           = new List<Account>();

        public ICollection<Transaction> Transactions { get; set; }
           = new List<Transaction>();
    }
}
