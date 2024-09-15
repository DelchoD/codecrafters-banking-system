using BankingManagementSystem.Core.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Transaction
{
    using static Constants.ErrorMessages;
    using static Constants.ValidationConstants;
    
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
        public AccountTransactionDto IBANFrom { get; set; } = null!;


        [Required]
        public AccountTransactionDto IBANTo { get; set; } = null!;

        

    }
}
