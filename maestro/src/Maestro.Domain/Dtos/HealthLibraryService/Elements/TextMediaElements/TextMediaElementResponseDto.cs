using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.TextMediaElements
{
    /// <summary>
    /// TextMediaElementResponseDto.
    /// </summary>
    public class TextMediaElementResponseDto : ElementDto
    {
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }

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
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        public MediaResponseDto Media { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public LocalizedStringWithAudioFileMediaResponseDto Text { get; set; }
    }
}