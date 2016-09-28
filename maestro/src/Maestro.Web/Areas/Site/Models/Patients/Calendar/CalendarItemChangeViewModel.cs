using System;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    public class CalendarItemChangeViewModel : CalendarChangeViewModel
    {
        /// <summary>
        /// New value of date when event will be started.
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// New value of date when event will be started.
        /// </summary>
        public DateTime? PrevDueDate { get; set; }

        /// <summary>
        /// Action which was performed.
        /// </summary>
        public CalendarItemAction Action { get; set; }

        /// <summary>
        /// Min start date from requrrences in current version of event.
        /// </summary>
        public DateTime? RecurrenceStartDate { get; set; }

        /// <summary>
        /// Max end date from requrrences in current version of event.
        /// </summary>
        public DateTime? RecurrenceEndDate { get; set; }

        /// <summary>
        /// Min start date from recurrences in event before update.
        /// </summary>
        public DateTime? PrevRecurrenceStartDate { get; set; }

        /// <summary>
        /// Max end date from recurrences in event before update.
        /// </summary>
        public DateTime? PrevRecurrenceEndDate { get; set; }
    }
}