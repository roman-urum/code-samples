using System;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Model for response with details of changes in calendar item.
    /// </summary>
    public class CalendarItemChangeResponseDto : CalendarChangeResponseDto
    {
        /// <summary>
        /// New value of date when event will be started.
        /// </summary>
        public DateTime? DueUtc { get; set; }

        /// <summary>
        /// New value of date when event will be started.
        /// </summary>
        public DateTime? PrevDueUtc { get; set; }

        /// <summary>
        /// Action which was performed.
        /// </summary>
        public CalendarItemAction Action { get; set; }

        /// <summary>
        /// Min start date from requrrences in current version of event.
        /// </summary>
        public DateTime? RecurrenceStartDateUtc { get; set; }

        /// <summary>
        /// Max end date from requrrences in current version of event.
        /// </summary>
        public DateTime? RecurrenceEndDateUtc { get; set; }

        /// <summary>
        /// Min start date from recurrences in event before update.
        /// </summary>
        public DateTime? PrevRecurrenceStartDateUtc { get; set; }

        /// <summary>
        /// Max end date from recurrences in event before update.
        /// </summary>
        public DateTime? PrevRecurrenceEndDateUtc { get; set; }
    }
}
