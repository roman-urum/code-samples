using Maestro.Web.DataAnnotations;

namespace Maestro.Web.Models.Users
{
    /// <summary>
    /// UserViewModel.
    /// </summary>
    /// <seealso cref="Maestro.Web.Models.Users.BaseUserViewModel" />
    public class UserViewModel : BaseUserViewModel
    {
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        [DisplayNameLocalized("CreateAdmin_RoleTitle")]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the token service user identifier.
        /// </summary>
        /// <value>
        /// The token service user identifier.
        /// </value>
        public string TokenServiceUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email verified.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is email verified; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmailVerified { get; set; }
    }
}