using System;

namespace Maestro.Web.Areas.Site.Models.Patients.Charts
{
    /// <summary>
    /// BaseReadingViewModel.
    /// </summary>
    public class BaseReadingViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTimeOffset? Date { get; set; }

        /// <summary>
        /// Gets or sets the alert.
        /// </summary>
        /// <value>
        /// The alert.
        /// </value>
        public AlertViewModel Alert { get; set; }
    }
}