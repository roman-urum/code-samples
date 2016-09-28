using System;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    /// <summary>
    /// RecurrenceDto.
    /// </summary>
    public class RecurrenceDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

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
        /// Gets or sets the every days.
        /// </summary>
        /// <value>
        /// The every days.
        /// </value>
        public int EveryDays { get; set; }
    }
}