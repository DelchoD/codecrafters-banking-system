using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;

    public class Account
    {
        [Key]
        [MaxLength(IdNumberMaxLength)]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        [Required]
        [MaxLength(AccountIbanMaxLength)]
        [MinLength(AccountIbanMinLength)]
        public string Iban { get; set; } = string.Empty;

        [Required]
        [MaxLength(AccountNameMaxLength)]
        [MinLength(AccountNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        public List<Transaction> TransactionsFrom { get; set; } = new();

        public List<Transaction> TransactionsTo { get; set; } = new();
    }
}