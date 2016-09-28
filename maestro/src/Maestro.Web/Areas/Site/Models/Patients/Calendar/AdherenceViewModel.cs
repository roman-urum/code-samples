using System;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// Model for adherence details.
    /// </summary>
    public class AdherenceViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the scheduled UTC.
        /// </summary>
        /// <value>
        /// The scheduled UTC.
        /// </value>
        public DateTimeOffset Scheduled { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time.
        /// </summary>
        /// <value>
        /// The expiration.
        /// </value>
        public DateTimeOffset? Expiration { get; set; }

        /// <summary>
        /// Gets or sets the updated date and time.
        /// </summary>
        /// <value>
        /// The updated UTC.
        /// </value>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public AdherenceStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the health session identifier.
        /// </summary>
        /// <value>
        /// The health session identifier.
        /// </value>
        public Guid? HealthSessionId { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the calendar event.
        /// </summary>
        /// <value>
        /// The calendar event.
        /// </value>
        public CalendarItemViewModel CalendarEvent { get; set; }
    }
}