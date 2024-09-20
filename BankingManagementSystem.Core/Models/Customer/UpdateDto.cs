using System.ComponentModel.DataAnnotations;
using BankingManagementSystem.Infrastructure.Data.Constants;

namespace BankingManagementSystem.Core.Models.Customer
{
    using static ErrorMessages;
    using static ValidationConstants;

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