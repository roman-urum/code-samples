using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    public class ProgramElementResponseDto : ProgramElementDto
    {
        /// <summary>
        /// Name of assigned protocol.
        /// </summary>
        public LocalizedStringResponseDto ProtocolName { get; set; }
    }
}
