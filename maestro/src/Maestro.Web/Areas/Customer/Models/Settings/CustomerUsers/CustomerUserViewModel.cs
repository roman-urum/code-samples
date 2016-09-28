using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers
{
    /// <summary>
    /// CustomerUserModel.
    /// </summary>
    public class CustomerUserViewModel
    {
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

        /// <summary>
        /// Gets or sets the customer role identifier.
        /// </summary>
        /// <value>
        /// The customer role identifier.
        /// </value>
        [DisplayNameLocalized("CustomerUser_CustomerRoleTitle")]
        public Guid CustomerUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        public List<Guid> Sites { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [DisplayNameLocalized("StateFieldLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [DisplayNameLocalized("CityFieldLabel")]
        [StringLength(50, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        [DisplayNameLocalized("Address1FieldLabel")]
        [StringLength(250, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>
        /// The Address2.
        /// </value>
        [DisplayNameLocalized("Address2FieldLabel")]
        [StringLength(250, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3.
        /// </summary>
        /// <value>
        /// The Address3.
        /// </value>
        [DisplayNameLocalized("Address3FieldLabel")]
        [StringLength(250, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [DisplayNameLocalized("ZipCodeFieldLabel")]
        [StringLength(10, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the National Provider Identificator.
        /// </summary>
        /// <value>
        /// The National Provider Identificator.
        /// </value>
        [DisplayNameLocalized("NationalProviderIdentificatorFieldLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        [RegularExpressionLocalized(@"^[a-zA-Z0-9\-]+$", "NationalProviderIdentificatorFieldSpecCharactersError")]
        public string NationalProviderIdentificator { get; set; }

        /// <summary>
        /// Gets or sets the customer user identifier.
        /// </summary>
        /// <value>
        /// The customer user identifier.
        /// </value>
        [DisplayNameLocalized("CustomerUserIdFieldLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        [RegularExpressionLocalized(@"^\d*$", "CustomerUserIdFieldSpecCharactersError")]
        public string CustomerUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [do not send invitation].
        /// </summary>
        /// <value>
        /// <c>true</c> if [do not send invitation]; otherwise, <c>false</c>.
        /// </value>
        public bool DoNotSendInvitation { get; set; }
    }
}