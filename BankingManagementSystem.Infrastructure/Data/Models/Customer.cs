using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;

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
        public long PersonalIdNumber { get; set; }

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
