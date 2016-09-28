using System.Collections.Generic;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// Class VitalChartSetting.
    /// </summary>
    public class VitalChartSetting : ChartSetting
    {
        /// <summary>
        /// Gets or sets the name of the vital.
        /// </summary>
        /// <value>The name of the vital.</value>
        public VitalType VitalName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show minimum].
        /// </summary>
        /// <value><c>true</c> if [show minimum]; otherwise, <c>false</c>.</value>
        public bool ShowMin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show maximum].
        /// </summary>
        /// <value><c>true</c> if [show maximum]; otherwise, <c>false</c>.</value>
        public bool ShowMax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show average].
        /// </summary>
        /// <value><c>true</c> if [show average]; otherwise, <c>false</c>.</value>
        public bool ShowAverage { get; set; }

        /// <summary>
        /// Gets or sets the display thresholds.
        /// </summary>
        /// <value>The display thresholds.</value>
        public virtual ICollection<DisplayThresholdSetting> DisplayThresholds { get; set; }
    }
}
