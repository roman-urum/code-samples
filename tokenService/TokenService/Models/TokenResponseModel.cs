using System;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// TokenResponseModel.
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// Utc date and time when current credential is expires.
        /// </summary>
        public DateTime? ExpirationUtc { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TokenResponseModel"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// </summary>
        /// <value>
        /// The TTL.
        /// </value>
        public int TTL { get; set; }
    }
}