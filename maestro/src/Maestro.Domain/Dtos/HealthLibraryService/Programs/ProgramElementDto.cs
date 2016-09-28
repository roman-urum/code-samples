using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    /// <summary>
    /// ProgramElementDto.
    /// </summary>
    public class ProgramElementDto
    {
        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>
        /// The protocol identifier.
        /// </value>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets the program day elements.
        /// </summary>
        /// <value>
        /// The program day elements.
        /// </value>
        public IList<ProgramDayElementDto> ProgramDayElements { get; set; }
    }
}