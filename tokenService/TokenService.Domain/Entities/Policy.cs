using System.Collections.Generic;
using CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities.Enums;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities
{
    /// <summary>
    /// Policy.
    /// </summary>
    public class Policy : AggregateRoot
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the effect.
        /// </summary>
        /// <value>
        /// The effect.
        /// </value>
        public PolicyEffects Effect { get; set; }

        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        /// <value>
        /// The service.
        /// </value>
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        public string Controller { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public Actions Action { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public int? Customer { get; set; }

        /// <summary>
        /// List of principals that use current policy.
        /// </summary>
        public virtual ICollection<Principal> Principals { get; set; }

        /// <summary>
        /// List of groups that use current policy.
        /// </summary>
        public virtual ICollection<Group> Groups { get; set; }
    }
}