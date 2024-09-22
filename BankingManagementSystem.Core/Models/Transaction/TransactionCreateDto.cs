using System.ComponentModel.DataAnnotations;
using BankingManagementSystem.Infrastructure.Data.Constants;

namespace BankingManagementSystem.Core.Models.Transaction
{
    using static ErrorMessages;
    using static ValidationConstants;
    
    public class TransactionCreateDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
          TransactionReasonMaxLength,
          MinimumLength = TransactionReasonMinLength,
          ErrorMessage = StringLengthErrorMessage
       )]
        public string Reason { get; set; } = string.Empty;

        [Required]
        [MaxLength(AccountIbanMaxLength)]
        public string IbanFrom { get; set; } = null!;

        [Required]
        [MaxLength(AccountIbanMaxLength)]
        public string IbanTo { get; set; } = null!;
    }
}