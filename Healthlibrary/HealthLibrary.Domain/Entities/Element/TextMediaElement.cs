using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// TextMediaElement.
    /// </summary>
    public class TextMediaElement : Element
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public MediaType? MediaType { get; set; }

        /// <summary>
        /// Gets or sets the text media elements to medias.
        /// </summary>
        /// <value>
        /// The text media elements to medias.
        /// </value>
        public virtual ICollection<TextMediaElementToMedia> TextMediaElementsToMedias { get; set; }

        /// <summary>
        /// Gets or sets the localized strings.
        /// </summary>
        /// <value>
        /// The localized strings.
        /// </value>
        public virtual ICollection<TextMediaElementString> TextLocalizedStrings { get; set; }
    }
}