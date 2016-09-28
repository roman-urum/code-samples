using System;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// Class DisplayThresholdSetting.
    /// </summary>
    public class DisplayThresholdSetting:Entity
    {
        /// <summary>
        /// Gets or sets the threshold identifier.
        /// </summary>
        /// <value>The threshold identifier.</value>
        public Guid ThresholdId { get; set; }

        /// <summary>
        /// Gets or sets the chart setting identifier.
        /// </summary>
        /// <value>The chart setting identifier.</value>
        public Guid VitalChartSettingId { get; set; }

        /// <summary>
        /// Gets or sets the chart setting.
        /// </summary>
        /// <value>The chart setting.</value>
        public VitalChartSetting VitalChartSetting { get; set; }
    }
}
