using System;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities
{
    /// <summary>
    /// Credential.
    /// </summary>
    /// <seealso cref="CareInnovations.HealthHarmony.Maestro.TokenService.Domain.AggregateRoot" />
    public class Credential : AggregateRoot
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public CredentialTypes Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Credential"/> is disabled.
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
        /// Gets or sets the principal identifier.
        /// </summary>
        /// <value>
        /// The principal identifier.
        /// </value>
        public Guid PrincipalId { get; set; }

        /// <summary>
        /// Gets or sets the principal.
        /// </summary>
        /// <value>
        /// The principal.
        /// </value>
        public virtual Principal Principal { get; set; }
    }
}
