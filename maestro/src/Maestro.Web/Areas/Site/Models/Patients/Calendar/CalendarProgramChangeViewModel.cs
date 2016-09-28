using System;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// View model for calendar program changes.
    /// </summary>
    public class CalendarProgramChangeViewModel : CalendarChangeViewModel
    {
        /// <summary>
        /// New value of date when event will be started.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Action which was performed.
        /// </summary>
        public CalendarProgramAction Action { get; set; }

        /// <summary>
        /// Number of day in program from which program will be started.
        /// </summary>
        public int StartDay { get; set; }

        /// <summary>
        /// Number of day in program on which program will be ended.
        /// </summary>
        public int EndDay { get; set; }

        /// <summary>
        /// Date from which program will be/was terminated.a
        /// </summary>
        public DateTime? TerminationDate { get; set; }
    }
}