using System;
using System.Collections.Generic;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// Class TrendSetting.
    /// </summary>
    public class TrendSetting : Entity
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the last days.
        /// </summary>
        /// <value>The last days.</value>
        public int? LastDays { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>The patient identifier.</value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the chart settings.
        /// </summary>
        /// <value>The chart settings.</value>
        public virtual ICollection<ChartSetting> ChartsSettings { get; set; }
    }
}