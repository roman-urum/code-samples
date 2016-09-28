using System;

namespace Maestro.Domain.Dtos.HealthLibraryService.Protocols
{
    /// <summary>
    /// ProtocolElementRequestDto.
    /// </summary>
    public class ProtocolElementRequestDto : BaseProtocolElementDto
    {
        /// <summary>
        /// Gets or sets the element identifier.
        /// </summary>
        /// <value>
        /// The element identifier.
        /// </value>
        public Guid ElementId { get; set; }
    }
}