using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Element
{
    /// <summary>
    /// Media.
    /// </summary>
    public class Media : CustomerAggregateRoot, ISoftDelitable
    {
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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the length of the content.
        /// </summary>
        /// <value>
        /// The length of the content.
        /// </value>
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the original storage key.
        /// </summary>
        /// <value>
        /// The original storage key.
        /// </value>
        public string OriginalStorageKey { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail storage key.
        /// </summary>
        /// <value>
        /// The thumbnail storage key.
        /// </value>
        public string ThumbnailStorageKey { get; set; }

        /// <summary>
        /// Gets or sets the localized strings.
        /// </summary>
        /// <value>
        /// The localized strings.
        /// </value>
        public virtual ICollection<LocalizedString> LocalizedStrings { get; set; }

        /// <summary>
        /// Gets or sets the text media elements to medias.
        /// </summary>
        /// <value>
        /// The text media elements to medias.
        /// </value>
        public virtual ICollection<TextMediaElementToMedia> TextMediaElementsToMedias { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}