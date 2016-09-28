using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// Contains base set of fields for principal.
    /// </summary>
    public abstract class BasePrincipalModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [MaxLength(DbConstraints.MaxLength.Username)]
        public string Username { get; set; }

        /// <summary>
        /// First name of principal user.
        /// </summary>
        [Required]
        [MaxLength(DbConstraints.MaxLength.FirstName)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of principal user.
        /// </summary>
        [Required]
        [MaxLength(DbConstraints.MaxLength.LastName)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [MaxLength(DbConstraints.MaxLength.PrincipalDescription)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BasePrincipalModel"/> is disabled.
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
        /// Gets or sets the locked out until UTC.
        /// </summary>
        /// <value>
        /// The locked out until UTC.
        /// </value>
        public DateTime? LockedOutUntilUtc { get; set; }

        /// <summary>
        /// Gets or sets the policies.
        /// </summary>
        /// <value>
        /// The policies.
        /// </value>
        public List<PolicyModel> Policies { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        /// <value>
        /// The groups.
        /// </value>
        public List<Guid> Groups { get; set; }
    }
}