using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Account
{
    using static BankingManagementSystem.Core.Constants.ErrorMessages;
    using static BankingManagementSystem.Core.Constants.ValidationConstants;
    public class AccountFormDTO
    {
        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            AccountIBANMaxLength,
            MinimumLength = AccountIBANMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string IBAN { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            AccountNameMaxLength,
            MinimumLength = AccoutNameMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string Name { get; set; } = string.Empty;


        public decimal? Balance { get; set; }

        public string? CreatorId { get; set; }
    }
}
