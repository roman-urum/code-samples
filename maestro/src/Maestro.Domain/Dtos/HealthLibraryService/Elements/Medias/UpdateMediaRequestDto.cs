using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias
{
    /// <summary>
    /// Dto to update data of existed media.
    /// </summary>
    [JsonObject]
    public class UpdateMediaRequestDto
    {
        /// <summary>
        /// Id of existed media entity.
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// File content in base-64 string.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Name of file to create.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File name
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