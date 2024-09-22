using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Data.Constants.ValidationConstants;

    public class Transaction
    {
        [Key]
        [MaxLength(IdNumberMaxLength)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(TransactionReasonMaxLength)]
        public string Reason { get; set; } = string.Empty;


        [Required]
        [MaxLength(AccountIbanMaxLength)]
        public string IBANFromId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(IBANFromId))]
        public Account IBANFrom { get; set; } = null!;


        [Required]
        public string IBANToId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(IBANToId))]
        public Account IBANTo { get; set; } = null!;


        [MaxLength(AccountIbanMaxLength)]
        public string IBANToId { get; set; } = string.Empty;

    }
}
