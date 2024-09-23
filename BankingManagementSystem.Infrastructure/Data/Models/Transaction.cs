using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;

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
        public string IbanFrom { get; set; } = string.Empty;

        [Required]
        [MaxLength(AccountIbanMaxLength)]
        public string IbanTo { get; set; } = string.Empty;
    }
}