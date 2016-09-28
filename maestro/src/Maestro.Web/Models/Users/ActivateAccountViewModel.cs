using System.ComponentModel.DataAnnotations;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Models.Users
{
    public class ActivateAccountViewModel : ActivationLinkViewModel
    {
        public const string IncorrectCredentialsKey = "IncorrectCredentials";

        public string ReturnUrl { get; set; }

        [DisplayNameLocalized("Users_ActivateAccount_PasswordFieldTitle")]
        [RequiredLocalized("Users_ActivateAccount_PasswordRequiredError")]
        [MinLength(AccountConstraints.PasswordMinLength,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "PasswordLength_ErrorMessage")]
        [RegularExpression(AccountConstraints.PasswordComplexityPattern,
            ErrorMessage = null,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "PasswordComplexityRegex_ErrorMessage")]
        public string Password { get; set; }

        [DisplayNameLocalized("Users_ActivateAccount_ConfirmPasswordFieldTitle")]
        [RequiredLocalized("Users_ActivateAccount_PasswordRequiredError")]
        [Compare("Password", ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "Users_ActivateAccount_PasswordNotMatched")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}