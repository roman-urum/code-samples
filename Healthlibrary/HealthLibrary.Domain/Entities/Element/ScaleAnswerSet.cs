using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// ScaleAnswerSet.
    /// </summary>
    public class ScaleAnswerSet : AnswerSet
    {
        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>
        /// The low value.
        /// </value>
        public int LowValue { get; set; }

        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>
        /// The high value.
        /// </value>
        public int HighValue { get; set; }

        /// <summary>
        /// Gets or sets the low label scale answer set strings.
        /// </summary>
        /// <value>
        /// The low label scale answer set strings.
        /// </value>
        public virtual ICollection<LowLabelScaleAnswerSetString> LowLabelScaleAnswerSetStrings { get; set; }

        /// <summary>
        /// Gets or sets the mid label scale answer set strings.
        /// </summary>
        /// <value>
        /// The mid label scale answer set strings.
        /// </value>
        public virtual ICollection<MidLabelScaleAnswerSetString> MidLabelScaleAnswerSetStrings { get; set; }

        /// <summary>
        /// Gets or sets the high label scale answer set strings.
        /// </summary>
        /// <value>
        /// The high label scale answer set strings.
        /// </value>
        public virtual ICollection<HighLabelScaleAnswerSetString> HighLabelScaleAnswerSetStrings { get; set; }
    }
}