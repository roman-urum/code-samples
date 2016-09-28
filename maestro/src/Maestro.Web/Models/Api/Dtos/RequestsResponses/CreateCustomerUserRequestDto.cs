using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maestro.Domain.Constants;
using Maestro.Web.DataAnnotations;
using Maestro.Web.Models.Api.Dtos.Entities;
using Maestro.Web.Resources;

namespace Maestro.Web.Models.Api.Dtos.RequestsResponses
{
    /// <summary>
    /// CreateCustomerUserRequestDto.
    /// </summary>
    public class CreateCustomerUserRequestDto
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&0-9]+$",
            "CreateAdmin_FristNameValidationError")]
        [RequiredLocalized("CreateAdmin_FirstNameRequiredError")]
        [StringLength(
            50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [RegularExpressionLocalized(@"^[^!@#$%\^&0-9]+$",
            "CreateAdmin_LastNameValidationError")]
        [RequiredLocalized("CreateAdmin_LastNameRequiredError")]
        [StringLength(
            50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress(ErrorMessageResourceType = typeof(GlobalStrings), ErrorMessageResourceName = "EmailAddressAttribute_Invalid")]
        [RequiredLocalized("CreateAdmin_EmailRequiredError")]
        [StringLength(
            256,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [MaxLength(DbConstraints.MaxLenght.Phone, 
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the customer role identifier.
        /// </summary>
        /// <value>
        /// The customer role identifier.
        /// </value>
        [RequiredLocalized("CustomerUser_CustomerRoleIdRequiredError")]
        public Guid CustomerUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [StringLength(
            50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the Address1.
        /// </summary>
        /// <value>
        /// The Address1.
        /// </value>
        [StringLength(
            250,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the Address2.
        /// </summary>
        /// <value>
        /// The Address2.
        /// </value>
        [StringLength(
            250,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the Address3.
        /// </summary>
        /// <value>
        /// The Address3.
        /// </value>
        [StringLength(
            250,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [StringLength(
            10,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the National Provider Identificator.
        /// </summary>
        /// <value>
        /// The National Provider Identificator.
        /// </value>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        [RegularExpressionLocalized(@"^[a-zA-Z0-9\-]+$", "NationalProviderIdentificatorFieldSpecCharactersError")]
        public string NationalProviderIdentificator { get; set; }

        /// <summary>
        /// Gets or sets the customer user identifier.
        /// </summary>
        /// <value>
        /// The customer user identifier.
        /// </value>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError")]
        //[RegularExpressionLocalized(@"^\d*$", "CustomerUserIdFieldSpecCharactersError")]
        public string CustomerUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [do not send invitation].
        /// </summary>
        /// <value>
        /// <c>true</c> if [do not send invitation]; otherwise, <c>false</c>.
        /// </value>
        public bool DoNotSendInvitation { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        public IList<WebsiteDto> Sites { get; set; }
    }
}