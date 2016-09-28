using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.DetailedData
{
    /// <summary>
    /// HealthSessionDetailedDataGroupViewModel.
    /// </summary>
    public class HealthSessionDetailedDataGroupViewModel
    {
        /// <summary>
        /// Gets or sets the calendar item identifier.
        /// </summary>
        /// <value>
        /// The calendar item identifier.
        /// </value>
        public Guid? CalendarItemId { get; set; }

        /// <summary>
        /// Gets or sets the completed.
        /// </summary>
        /// <value>
        /// The completed.
        /// </value>
        public DateTimeOffset Completed { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public IList<HealthSessionDetailedDataGroupElementViewModel> Elements { get; set; }
    }
}