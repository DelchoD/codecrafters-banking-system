using BankingManagementSystem.Core.Models.Account;
using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Transaction
{
    using static BankingManagementSystem.Core.Constants.ErrorMessages;
    using static BankingManagementSystem.Core.Constants.ValidationConstants;


    public class TransactionCreateDTO
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
        public AccountTransactionDTO IBANFrom { get; set; } = null!;


        [Required]
        public AccountTransactionDTO IBANTo { get; set; } = null!;

        

    }
}
