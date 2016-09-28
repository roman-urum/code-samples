using System;
using System.ComponentModel.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// CalendarProgramViewModel.
    /// </summary>
    public class ScheduleCalendarProgramViewModel
    {
        /// <summary>
        /// Id of assigned program.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null)
        ]
        public Guid ProgramId { get; set; }

        /// <summary>
        /// The calendar date that represents the first day of a program.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null)
        ]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null)
        ]
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null)
        ]
        public int EndDay { get; set; }

        /// <summary>
        /// The number of minutes after which the calendar event should expire. 
        /// Optional. Default is null.
        /// </summary>
        public int? ExpireMinutes { get; set; }
    }
}