using System.ComponentModel.DataAnnotations;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Models.Users
{
    public class LoginViewModel
    {
        public const string IncorrectCredentialsKey = "IncorrectCredentials";

        public string ReturnUrl { get; set; }

        [DisplayNameLocalized("Users_Login_EmailFieldTitle")]
        [RequiredLocalized("Users_Login_EmailRequiredError")]
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "EmailAddressAttribute_Invalid")]
        public string Email { get; set; }

        [DisplayNameLocalized("Users_Login_PasswordFieldTitle")]
        [RequiredLocalized("Users_Login_PasswordRequiredError")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginViewModel()
        { }

        public LoginViewModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }
    }
}