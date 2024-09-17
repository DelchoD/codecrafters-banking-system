using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Customer
{
    using static Constants.ErrorMessages;
    using static Constants.ValidationConstants;

    public class UpdateDto
    {
        [Required(ErrorMessage = RequireErrorMessage)]
        [EmailAddress(ErrorMessage = InvalidFormatErrorMessage)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            PhoneMaxLength,
            MinimumLength = PhoneMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [MinLength(PasswordMinLength, ErrorMessage = StringCountCharactersErrorMessage)]
        public string Password { get; set; } = string.Empty;
    }
}