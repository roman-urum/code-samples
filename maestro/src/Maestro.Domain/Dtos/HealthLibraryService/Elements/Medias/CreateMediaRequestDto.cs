using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias
{
    /// <summary>
    /// Model for media content in requests.
    /// </summary>
    [JsonObject]
    public class CreateMediaRequestDto
    {
        /// <summary>
        /// File content in base-64 string.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the source content URL.
        /// </summary>
        /// <value>
        /// The source content URL.
        /// </value>
        public string SourceContentUrl { get; set; }

        /// <summary>
        /// Name of file to create.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The file name attached to media
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Description of media file.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of file content.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }
    }
}