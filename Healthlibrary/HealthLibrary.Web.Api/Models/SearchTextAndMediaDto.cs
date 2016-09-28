using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Web.Api.Models
{
    /// <summary>
    /// SearchTextAndMediaDto.
    /// </summary>
    public class SearchTextAndMediaDto : SearchEntryDto
    {
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
        /// Gets or sets the name of the image.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        public MediaType? MediaType { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }
    }
}