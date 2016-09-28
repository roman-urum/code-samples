using System;
using System.ComponentModel.DataAnnotations;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Models.Users
{
    /// <summary>
    /// BaseUserViewModel.
    /// </summary>
    public class BaseUserViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&0-9]+$", "CreateAdmin_FristNameValidationError")]
        [RequiredLocalized("CreateAdmin_FirstNameRequiredError")]
        [DisplayNameLocalized("CreateAdmin_FirstNameTitle")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&0-9]+$", "CreateAdmin_LastNameValidationError")]
        [RequiredLocalized("CreateAdmin_LastNameRequiredError")]
        [DisplayNameLocalized("CreateAdmin_LastNameTitle")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [RemoteWithoutArea("DoesEmailExist", "Account", AdditionalFields = "Id", ErrorMessage = "User with this email already exists")]
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "EmailAddressAttribute_Invalid")]
        [RequiredLocalized("CreateAdmin_EmailRequiredError")]
        [DisplayNameLocalized("CreateAdmin_EmailTitle")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [DisplayNameLocalized("CreateAdmin_PhoneTitle")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }
    }
}