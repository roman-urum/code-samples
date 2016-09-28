using System;
using System.Collections.Generic;

namespace HealthLibrary.Web.Api.Models.Elements.Medias
{
    /// <summary>
    /// Model for response with media info.
    /// </summary>
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
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>
        /// The name of the original file.
        /// </value>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Gets or sets the media URL.
        /// </summary>
        /// <value>
        /// The media URL.
        /// </value>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail URL.
        /// </summary>
        /// <value>
        /// The thumbnail URL.
        /// </value>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Type of file contet.
        /// </summary>
        public string ContentType { get; set; }

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
        public IList<string> Tags { get; set; }
    }
}