using Maestro.Web.DataAnnotations;

namespace Maestro.Web.Models.Users
{
    /// <summary>
    /// ActivationLinkViewModel.
    /// </summary>
    public class ActivationLinkViewModel
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [DisplayNameLocalized("Users_ActivateAccount_Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the expiration time in days.
        /// </summary>
        /// <value>
        /// The expires.
        /// </value>
        public int Expires { get; set; }

        /// <summary>
        /// Password expiration setting of user's customer.
        /// </summary>
        public int? PasswordExpiration { get; set; }
    }
}