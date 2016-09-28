using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// VitalsChartPointViewModel.
    /// </summary>
    public class VitalsChartPointViewModel : BaseChartPointViewModel
    {
        /// <summary>
        /// Gets or sets the average reading.
        /// </summary>
        /// <value>
        /// The average reading.
        /// </value>
        public decimal AvgReading { get; set; }

        /// <summary>
        /// Gets or sets the minimum reading.
        /// </summary>
        /// <value>
        /// The minimum reading.
        /// </value>
        public decimal MinReading { get; set; }

        /// <summary>
        /// Gets or sets the maximum reading.
        /// </summary>
        /// <value>
        /// The maximum reading.
        /// </value>
        public decimal MaxReading { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or sets the readings.
        /// </summary>
        /// <value>
        /// The readings.
        /// </value>
        public List<VitalReadingViewModel> Readings { get; set; }
    }
}