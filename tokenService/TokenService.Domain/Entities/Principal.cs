using System;
using System.Collections.Generic;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities
{
    /// <summary>
    /// Principal.
    /// </summary>
    /// <seealso cref="CareInnovations.HealthHarmony.Maestro.TokenService.Domain.AggregateRoot" />
    /// <seealso cref="CareInnovations.HealthHarmony.Maestro.TokenService.Domain.ISoftDelitable" />
    public class Principal : AggregateRoot, ISoftDelitable
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

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
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Principal"/> is disabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disabled; otherwise, <c>false</c>.
        /// </value>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the expires UTC.
        /// </summary>
        /// <value>
        /// The expires UTC.
        /// </value>
        public DateTime? ExpiresUtc { get; set; }

        /// <summary>
        /// Gets or sets the failed count.
        /// </summary>
        /// <value>
        /// The failed count.
        /// </value>
        public ulong FailedCount { get; set; }

        /// <summary>
        /// Gets or sets the locked out until UTC.
        /// </summary>
        /// <value>
        /// The locked out until UTC.
        /// </value>
        public DateTime? LockedOutUntilUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated UTC.
        /// </summary>
        /// <value>
        /// The updated UTC.
        /// </value>
        public DateTime UpdatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Returns string with user first and last name or null if these fields are empty.
        /// </summary>
        public string FullName
        {
            get
            {
                return string.IsNullOrEmpty(this.FirstName) || string.IsNullOrEmpty(this.LastName)
                    ? null
                    : string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
        public virtual ICollection<Credential> Credentials { get; set; }

        /// <summary>
        /// List of policies assigned to current principal.
        /// </summary>
        public virtual ICollection<Policy> Policies { get; set; }

        /// <summary>
        /// List of groups assigned to Principal.
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }
    }
}