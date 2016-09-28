using System.Collections.Generic;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// Condition.
    /// </summary>
    public class Condition : Entity
    {
        /// <summary>
        /// Gets or sets the customer identifier;
        /// </summary>
        /// <value>
        /// The customer identifier;
        /// </value>
        public int CustomerId { get; set; }

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
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the default thresholds.
        /// </summary>
        /// <value>
        /// The default thresholds.
        /// </value>
        public virtual ICollection<DefaultThreshold> DefaultThresholds { get; set; }

        /// <summary>
        /// Gets or sets the patient conditions.
        /// </summary>
        /// <value>
        /// The patient conditions.
        /// </value>
        public virtual ICollection<PatientCondition> PatientConditions { get; set; }
    }
}
