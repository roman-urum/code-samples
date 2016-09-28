using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// TextMediaElementString.
    /// </summary>
    public class TextMediaElementString : LocalizedString
    {
        /// <summary>
        /// Gets or sets the text media element identifier.
        /// </summary>
        /// <value>
        /// The text media element identifier.
        /// </value>
        public Guid? TextMediaElementId { get; set; }

        /// <summary>
        /// Gets or sets the text media element.
        /// </summary>
        /// <value>
        /// The text media element.
        /// </value>
        public virtual TextMediaElement TextMediaElement { get; set; }
    }
}