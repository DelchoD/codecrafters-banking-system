using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.Customer
{
    using static BankingManagementSystem.Core.Constants.ErrorMessages;
    using static BankingManagementSystem.Core.Constants.ValidationConstants;
    public class CustomerFormDTO
    {

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            CustomerFirstNameMaxLength,
            MinimumLength = CustomerFirstNameMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
           CustomerMiddleNameMaxLength,
           MinimumLength = CustomerMiddleNameMinLength,
           ErrorMessage = StringLengthErrorMessage
        )]
        public string MiddleName { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
                  CustomerLastNameMaxLength,
                  MinimumLength = CustomerLastNameMinLength,
                  ErrorMessage = StringLengthErrorMessage
        )]
        public string LastName { get; set; } = string.Empty;



        [Required(ErrorMessage = RequireErrorMessage)]
        [EmailAddress(ErrorMessage = InvalidFormatErrorMessage)]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
            CustomerPhoneMaxLength,
            MinimumLength = CustomerPhoneMinLength,
            ErrorMessage = StringLengthErrorMessage
         )]
        public string PhoneNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        [MinLength(CustomerPasswordMinLength, ErrorMessage = StringCountCharactersErrorMessage)]
        public string Password { get; set; } = string.Empty;



        [Required(ErrorMessage = RequireErrorMessage)]
        [MinLength(CustomerIdNumberMinLength, ErrorMessage = StringCountCharactersErrorMessage)]
        public string PersonalIDNumber { get; set; } = string.Empty;


        [Required(ErrorMessage = RequireErrorMessage)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(
           CustomerAddressMaxLength,
           MinimumLength = CustomerAddressMinLength,
           ErrorMessage = StringLengthErrorMessage
        )]
        public string Address { get; set; } = string.Empty;


        public string? CreatorId { get; set; }

    }
}
