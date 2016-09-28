using System;

namespace Maestro.Web.Areas.Customer.Models.Settings.CustomerUsers
{
    /// <summary>
    /// UserListViewModel.
    /// </summary>
    public class CustomerUserListViewModel : CustomerUserViewModel
    {
        /// <summary>
        /// Id of customer user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is email verified.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is email verified; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmailVerified { get; set; }
    }
}