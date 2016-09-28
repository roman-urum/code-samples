using System.Web;
using Maestro.Web.DataAnnotations;

namespace Maestro.Web.Areas.Customer.Models.Settings.General
{
    /// <summary>
    /// GeneralSettingsViewModel.
    /// </summary>
    public class GeneralSettingsViewModel
    {
        // in bytes
        private const int MaxLogoSize = 102400;

        public const string ServiceErrorMessageKey = "ServiceErrorMessage";

        //[RemoteWithoutArea("IsCustomerNameExists", "Customers", AdditionalFields = "Id", 
        //    ErrorMessageResourceType = typeof(GlobalStrings), 
        //    ErrorMessageResourceName = "Edit_Customer_NameExistsMessage")]
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        [DisplayNameLocalized("Edit_Customer_CustomerName")]
        [RequiredLocalized("Edit_Customer_NameFieldRequiredError")]
        [RegularExpressionLocalized(@"^[^!@#$%\^&]+$", "Edit_Customer_NameValidationError")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the logo image.
        /// </summary>
        /// <value>
        /// The logo image.
        /// </value>
        [DisplayNameLocalized("Edit_Customer_LogoField")]
        [UploadFileExtensions("png|gif", "Edit_Customer_LogoRequiredFormatText")]
        [UploadFileSize(MaxLogoSize, "Edit_Customer_LogoSizeError")]
        [ImageDimensions("Edit_Customer_LogoDimensionsError", MaxWidth = 600, MaxHeight = 200)]
        [NotificationMessage("Edit_Customer_LogoRequiredFormatText")]
        public HttpPostedFileBase LogoImage { get; set; }

        /// <summary>
        /// Gets or sets the logo path.
        /// </summary>
        /// <value>
        /// The logo path.
        /// </value>
        public string LogoPath { get; set; }

        /// <summary>
        /// Gets or sets the password expiration.
        /// </summary>
        /// <value>
        /// The password expiration.
        /// </value>
        [Integer("Edit_Customer_PasswordExpirationValidationError")]
        [DisplayNameLocalized("Edit_Customer_PasswordExpirationFieldTitle")]
        [RangeLocalized(1, 9999, "Edit_Customer_PasswordExpirationValidationError")]
        [RequiredLocalized("Edit_Customer_PasswordExpirationRequiredError")]
        [NotificationMessage("Edit_Customer_PasswordExpirationNotification")]
        public int PasswordExpiration { get; set; }

        /// <summary>
        /// Gets or sets the idle session timeout.
        /// </summary>
        /// <value>
        /// The idle session timeout.
        /// </value>
        [Integer("Edit_Customer_IdleSessionTimeoutFieldValidationError")]
        [RangeLocalized(5, 20, "Edit_Customer_IdleSessionTimeoutFieldValidationError")]
        [DisplayNameLocalized("Edit_Customer_IdleSessionTimeoutFieldTitle")]
        [RequiredLocalized("Edit_Customer_IdleSessionTimeoutRequiredError")]
        [NotificationMessage("Edit_Customer_IdleSessionTimeoutFieldNotification")]
        public int IdleSessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the subdomain.
        /// </summary>
        /// <value>
        /// The subdomain.
        /// </value>
        public string Subdomain { get; set; }
    }
}