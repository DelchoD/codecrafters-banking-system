using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Customer
{
    using static Constants.ErrorMessages;
    using static Constants.ValidationConstants;

    public class FormDto
    {
        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            FirstNameMaxLength,
            MinimumLength = FirstNameMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
           MiddleNameMaxLength,
           MinimumLength = MiddleNameMinLength,
           ErrorMessage = StringLengthErrorMessage
        )]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
                  LastNameMaxLength,
                  MinimumLength = LastNameMinLength,
                  ErrorMessage = StringLengthErrorMessage
        )]
        public string LastName { get; set; } = string.Empty;

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

        [Required(ErrorMessage = RequireErrorMessage)]
        [MinLength(PasswordMinLength, ErrorMessage = StringCountCharactersErrorMessage)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [MinLength(IdNumberMinLength, ErrorMessage = StringCountCharactersErrorMessage)]
        public string PersonalIDNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
           AddressMaxLength,
           MinimumLength = AddressMinLength,
           ErrorMessage = StringLengthErrorMessage
        )]
        public string Address { get; set; } = string.Empty;
    }
}