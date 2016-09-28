using System;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// LocalizedString.
    /// </summary>
    public abstract class LocalizedString : Entity
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the value of the string.
        /// </summary>
        /// <value>
        /// The value of the string.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the description of the string.
        /// </summary>
        /// <value>
        /// The description of the string.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the pronunciation.
        /// </summary>
        /// <value>
        /// The pronunciation.
        /// </value>
        public string Pronunciation { get; set; }

        /// <summary>
        /// Gets or sets the audio file media identifier.
        /// </summary>
        /// <value>
        /// The audio file media identifier.
        /// </value>
        public virtual Guid? AudioFileMediaId { get; set; }

        /// <summary>
        /// Gets or sets the audio file media.
        /// </summary>
        /// <value>
        /// The audio file media.
        /// </value>
        public virtual Media AudioFileMedia { get; set; }
    }
}