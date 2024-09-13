using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;

    public class Account
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [MaxLength(AccountIBANMaxLength)]
        public string Iban { get; set; } = string.Empty;

        [Required]
        [MaxLength(AccountNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [InverseProperty(nameof(Transaction.IBANFrom))]
        public List<Transaction> TransactionsFrom { get; set; }
            = new List<Transaction>();

        [InverseProperty(nameof(Transaction.IBANTo))]
        public List<Transaction> TransactionsTo { get; set; }
         = new List<Transaction>();
    }
}