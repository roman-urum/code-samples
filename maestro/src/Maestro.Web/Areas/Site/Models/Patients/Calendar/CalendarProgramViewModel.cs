using System;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// View model for response with calendar program details.
    /// </summary>
    public class CalendarProgramViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public Guid ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        /// <value>
        /// The name of the program.
        /// </value>
        public string ProgramName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date UTC.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        public int EndDay { get; set; }

        /// <summary>
        /// The number of minutes after which the calendar event should expire. 
        /// Optional. Default is null.
        /// </summary>
        public int? ExpireMinutes { get; set; }
    }
}