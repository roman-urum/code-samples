using Maestro.Domain.Dtos.HealthLibraryService.Elements;

namespace Maestro.Domain.Dtos.HealthLibraryService.Protocols
{
    /// <summary>
    /// ProtocolElementResponseDto.
    /// </summary>
    public class ProtocolElementResponseDto : BaseProtocolElementDto
    {
        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public ElementDto Element { get; set; }
    }
}