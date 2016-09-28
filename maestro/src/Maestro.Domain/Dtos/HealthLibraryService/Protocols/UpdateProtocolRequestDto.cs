using System;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Protocols
{
    /// <summary>
    /// UpdateProtocolRequestDto.
    /// </summary>
    [JsonObject]
    public class UpdateProtocolRequestDto : CreateProtocolRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }
    }
}