using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Core.Models.User
{
    using static BankingManagementSystem.Core.Constants.ErrorMessages;
    using static BankingManagementSystem.Core.Constants.ValidationConstants;

    public class CustomerUpdateDTO
    {
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
    }
}
