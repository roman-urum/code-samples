using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// BaseAnswerStatisticViewModel.
    /// </summary>
    public class BaseAnswerStatisticViewModel
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }


        /// <summary>
        /// Gets or sets the list of question readings.
        /// </summary>
        /// <value>
        /// The list of question readings.
        /// </value>
        public IList<QuestionReadingViewModel> Readings { get; set; }

        /// <summary>
        /// Gets or sets the recent readings date range.
        /// </summary>
        /// <value>
        /// The recent readings date range. 
        /// </value>
        public ChartDateRangeViewModel ReadingsDateRange { get; set; }
    }
}