using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// TextMediaElementToMedia.
    /// </summary>
    public class TextMediaElementToMedia
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the text media element identifier.
        /// </summary>
        /// <value>
        /// The text media element identifier.
        /// </value>
        public virtual Guid TextMediaElementId { get; set; }

        /// <summary>
        /// Gets or sets the text media element.
        /// </summary>
        /// <value>
        /// The text media element.
        /// </value>
        public virtual TextMediaElement TextMediaElement { get; set; }

        /// <summary>
        /// Gets or sets the media identifier.
        /// </summary>
        /// <value>
        /// The media identifier.
        /// </value>
        public virtual Guid MediaId { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        public virtual Media Media { get; set; }
    }
}