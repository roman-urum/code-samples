using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Protocols
{
    /// <summary>
    /// SearchProtocolsRequestDto.
    /// </summary>
    [JsonObject]
    public class SearchProtocolsRequestDto
    {
        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }
    }
}