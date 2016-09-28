using System;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    /// <summary>
    /// ProgramDayElementToProgramElementDto.
    /// </summary>
    public class ProgramDayElementToProgramElementDto
    {
        /// <summary>
        /// Gets or sets the program day element identifier.
        /// </summary>
        /// <value>
        /// The program day element identifier.
        /// </value>
        public Guid ProgramDayElementId { get; set; }

        /// <summary>
        /// Gets or sets the program element identifier.
        /// </summary>
        /// <value>
        /// The program element identifier.
        /// </value>
        public Guid ProgramElementId { get; set; }
    }
}