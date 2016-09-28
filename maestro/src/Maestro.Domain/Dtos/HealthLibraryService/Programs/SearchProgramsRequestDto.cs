using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    /// <summary>
    /// SearchProgramsRequestDtoю
    /// </summary>
    [JsonObject]
    public class SearchProgramsRequestDto
    {
        /// <summary>
        /// Gets or sets the skip.
        /// </summary>
        /// <value>
        /// The skip.
        /// </value>
        public int Skip { get; set; }

        /// <summary>
        /// Gets or sets the take.
        /// </summary>
        /// <value>
        /// The take.
        /// </value>
        public int Take { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }
    }
}