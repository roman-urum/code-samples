using System;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// Class QuestionChartSetting.
    /// </summary>
    public class QuestionChartSetting : ChartSetting
    {
        /// <summary>
        /// Gets or sets the question identifier.
        /// </summary>
        /// <value>The question identifier.</value>
        public Guid QuestionId { get; set; }
    }
}
