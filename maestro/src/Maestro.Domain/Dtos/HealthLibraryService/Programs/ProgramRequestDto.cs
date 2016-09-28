using System.Collections.Generic;

using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    /// <summary>
    /// ProgramRequestDto.
    /// </summary>
    [JsonObject]
    public class ProgramRequestDto : BaseProgramDto
    {
        /// <summary>
        /// Gets or sets the program elements.
        /// </summary>
        /// <value>
        /// The program elements.
        /// </value>
        public List<ProgramElementDto> ProgramElements { get; set; }
    }
}