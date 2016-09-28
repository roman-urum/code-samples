using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    /// <summary>
    /// ProgramResponseDto.
    /// </summary>
    public class ProgramResponseDto : BaseProgramDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the program elements.
        /// </summary>
        /// <value>
        /// The program elements.
        /// </value>
        public List<ProgramElementResponseDto> ProgramElements { get; set; }
    }
}