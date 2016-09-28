using System;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// CustomerUserSite.
    /// </summary>
    public class CustomerUserSite
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid CustomerUserId { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public Guid SiteId { get; set; }

        /// <summary>
        /// Gets or sets the customer user.
        /// </summary>
        /// <value>
        /// The customer user.
        /// </value>
        public virtual CustomerUser CustomerUser { get; set; }
    }
}