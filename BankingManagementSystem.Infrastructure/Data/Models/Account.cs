using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;

    public class Account
    {
        [Key]
        [MaxLength(AccountIBANMaxLength)]
        public string Iban { get; set; } = string.Empty;

        [Required]
        [MaxLength(AccountNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public long Id { get; set; }

        [Required]
        public long CustomerId { get; set; }

        [Required]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        [InverseProperty(nameof(Transaction.IBANFrom))]
        public ICollection<Transaction> TransactionsFrom { get; set; }
            = new List<Transaction>();

        [InverseProperty(nameof(Transaction.IBANTo))]
        public ICollection<Transaction> TransactionsTo { get; set; }
         = new List<Transaction>();
    }
}