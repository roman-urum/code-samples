using System.Collections.Generic;
using System.Runtime.Serialization;
using Maestro.Common.Extensions;

namespace Maestro.Web.Security
{
    using System;

    /// <summary>
    /// Container to store info about authenticated user.
    /// </summary>
    [Serializable]
    public class UserAuthData
    {
        /// <summary>
        /// Identifies if modal alert with notification that password expires was shown.
        /// </summary>
        public bool IsChangePasswordAlertShown { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Utc when user password expires.
        /// </summary>
        public DateTime? PasswordExpirationUtc { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }
        
        /// <summary>
        /// Gets or sets the customer role.
        /// </summary>
        /// <value>The customer role.</value>
        public string CustomerRole { get; set; }

        /// <summary>
        /// Default session timeout.
        /// </summary>
        public TimeSpan SessionTimeout { get; set; }

        /// <summary>
        /// Time where session expires next time.
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public PermissionsAuthData Permissions { get; set; }

        /// <summary>
        /// Contains list of ids for sites which accessible for authenticated user.
        /// </summary>
        public IEnumerable<Guid> Sites { get; set; }

        /// <summary>
        /// Returns full name of authorized user.
        /// </summary>
        [IgnoreDataMember]
        public string FullName
        {
            get { return "{0} {1}".FormatWith(FirstName, LastName); }
        }
    }
}