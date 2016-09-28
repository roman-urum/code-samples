using System;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// ProgramDayElementDto.
    /// </summary>
    public class ProgramDayElementDto
    {
        /// <summary>
        /// Gets or sets the recurrence identifier.
        /// </summary>
        /// <value>
        /// The recurrence identifier.
        /// </value>
        public Guid? RecurrenceId { get; set; }

        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        [Range(0, int.MaxValue, ErrorMessageResourceName = "ProgramDayElementDto_Day_ValidationError", ErrorMessageResourceType = typeof(GlobalStrings))]
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int? Sort { get; set; }
    }
}