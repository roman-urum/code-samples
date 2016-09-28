using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// BaseChartViewModel.
    /// </summary>
    public class BaseChartViewModel
    {
        /// <summary>
        /// Gets or sets the name of the chart.
        /// </summary>
        /// <value>
        /// The name of the chart.
        /// </value>
        public string ChartName { get; set; }

        /// <summary>
        /// Gets or sets the chart range.
        /// </summary>
        /// <value>
        /// The chart range.
        /// </value>
        public ChartDateRangeViewModel ChartRange { get; set; }

        /// <summary>
        /// Gets or sets the chart data.
        /// </summary>
        /// <value>
        /// The chart data.
        /// </value>
        public IList<BaseChartPointViewModel> ChartData { get; set; }
    }
}