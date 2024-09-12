using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Account
{
    using static BankingManagementSystem.Core.Constants.ErrorMessages;
    using static BankingManagementSystem.Core.Constants.ValidationConstants;
    public class AccountFormDto
    {
        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            AccountIbanMaxLength,
            MinimumLength = AccountIbanMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string Iban { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            AccountNameMaxLength,
            MinimumLength = AccountNameMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string Name { get; set; } = string.Empty;


        public decimal? Balance { get; set; }

        public string? CreatorId { get; set; }
    }
}
