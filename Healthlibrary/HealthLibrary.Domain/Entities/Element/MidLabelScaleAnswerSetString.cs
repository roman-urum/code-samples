using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// MidLabelScaleAnswerSetString.
    /// </summary>
    public class MidLabelScaleAnswerSetString : LocalizedString
    {
        /// <summary>
        /// Gets or sets the scale answer set identifier.
        /// </summary>
        /// <value>
        /// The scale answer set identifier.
        /// </value>
        public Guid? ScaleAnswerSetId { get; set; }

        /// <summary>
        /// Gets or sets the scale answer set.
        /// </summary>
        /// <value>
        /// The scale answer set.
        /// </value>
        public virtual ScaleAnswerSet ScaleAnswerSet { get; set; }
    }
}