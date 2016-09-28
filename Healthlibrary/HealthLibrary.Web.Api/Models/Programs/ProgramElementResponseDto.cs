using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// Dto to return program element data in response.
    /// </summary>
    public class ProgramElementResponseDto : ProgramElementDto
    {
        /// <summary>
        /// Name of assigned protocol.
        /// </summary>
        public LocalizedStringResponseDto ProtocolName { get; set; }
    }
}