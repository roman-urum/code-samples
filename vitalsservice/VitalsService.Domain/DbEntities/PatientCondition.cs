using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// PatientCondition.
    /// </summary>
    public class PatientCondition
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        public Guid ConditionId { get; set; }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public virtual Condition Condition { get; set; }
    }
}