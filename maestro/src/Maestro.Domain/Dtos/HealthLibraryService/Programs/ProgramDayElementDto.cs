using System;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
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