using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// DefaultThreshold.
    /// </summary>
    public class DefaultThreshold : Threshold
    {
        /// <summary>
        /// Gets or sets the default type.
        /// </summary>
        /// <value>
        /// The default type.
        /// </value>
        public string DefaultType { get; set; }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        public Guid? ConditionId { get; set; }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public Condition Condition { get; set; }
    }
}