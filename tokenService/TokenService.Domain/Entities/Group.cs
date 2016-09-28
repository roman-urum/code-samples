using System.Collections.Generic;

namespace CareInnovations.HealthHarmony.Maestro.TokenService.Domain.Entities
{
    /// <summary>
    /// Group.
    /// </summary>
    /// <seealso cref="CareInnovations.HealthHarmony.Maestro.TokenService.Domain.AggregateRoot" />
    /// <seealso cref="CareInnovations.HealthHarmony.Maestro.TokenService.Domain.ISoftDelitable" />
    public class Group : AggregateRoot, ISoftDelitable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public int? Customer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Group"/> is disabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disabled; otherwise, <c>false</c>.
        /// </value>
        public bool Disabled { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// List of principals added to current group.
        /// </summary>
        public virtual ICollection<Principal> Principals { get; set; } 
        
        /// <summary>
        /// List of policies assigned to current group.
        /// </summary>
        public virtual ICollection<Policy> Policies { get; set; }
    }
}