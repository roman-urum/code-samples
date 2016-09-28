using System.ComponentModel.DataAnnotations;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Models.Users
{
    /// <summary>
    /// Model for page to change expired password.
    /// </summary>
    public class ChangePasswordViewModel
    {
        public const string IncorrectCredentialsKey = "IncorrectCredentials";

        [DisplayNameLocalized("Users_Login_EmailFieldTitle")]
        [RequiredLocalized("Users_Login_EmailRequiredError")]
        public string Email { get; set; }

        [DisplayNameLocalized("Users_ChangePassword_CurrentPasswordFieldTitle")]
        [RequiredLocalized("Users_ChangePassword_CurrentPasswordRequiredError")]
        public string CurrentPassword { get; set; }

        [DisplayNameLocalized("Users_ChangePassword_NewPasswordFieldTitle")]
        [RequiredLocalized("Users_ChangePassword_NewPasswordRequiredError")]
        [MinLength(AccountConstraints.PasswordMinLength,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "PasswordLength_ErrorMessage")]
        [RegularExpression(AccountConstraints.PasswordComplexityPattern,
            ErrorMessage = null,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "PasswordComplexityRegex_ErrorMessage")]
        public string NewPassword { get; set; }

        [DisplayNameLocalized("Users_ChangePassword_ConfirmNewPasswordFieldTitle")]
        [RequiredLocalized("Users_ChangePassword_ConfirmNewPasswordRequiredError")]
        [Compare("NewPassword", 
            ErrorMessageResourceName = "Users_ChangePassword_ConfirmPasswordNotMatchError", 
            ErrorMessageResourceType = typeof(GlobalStrings))]
        public string ConfirmNewPassword { get; set; }
    }
}