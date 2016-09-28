using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias
{
    /// <summary>
    /// Model for response with media info.
    /// </summary>
    [JsonObject]
    public class MediaResponseDto
    {
        /// <summary>
        /// Id of media.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the media URL.
        /// </summary>
        /// <value>
        /// The media URL.
        /// </value>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the media thumbnail url
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Type of file contet.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the media original file name
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Description about file content.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }
    }
}