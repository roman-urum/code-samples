using System;
using System.Collections.Generic;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Models
{
    /// <summary>
    /// CachedPrincipalModel.
    /// </summary>
    public class CachedPrincipalModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CachedPrincipalModel"/> class.
        /// </summary>
        public CachedPrincipalModel()
        {
            Policies = new List<CachedPolicyModel>();
        }

        /// <summary>
        /// Gets or sets the principal identifier.
        /// </summary>
        /// <value>
        /// The principal identifier.
        /// </value>
        public Guid PrincipalId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        /// Date in ticks when principal was updated last time.
        /// </summary>
        /// <value>
        /// The principal updated UTC.
        /// </value>
        public long UpdatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the policies.
        /// </summary>
        /// <value>
        /// The policies.
        /// </value>
        public List<CachedPolicyModel> Policies { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// Note: Will be used only during certificate based auth.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }
    }
}