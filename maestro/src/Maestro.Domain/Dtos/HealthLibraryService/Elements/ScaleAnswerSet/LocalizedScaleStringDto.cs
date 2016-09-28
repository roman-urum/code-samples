using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet
{
    /// <summary>
    /// LocalizedScaleStringDto.
    /// </summary>
    public class LocalizedScaleStringDto
    {
        /// <summary>
        /// Gets or sets the audio file media.
        /// </summary>
        /// <value>
        /// The audio file media.
        /// </value>
        public CreateMediaRequestDto AudioFileMedia { get; set; }

        /// <summary>
        /// Translate.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// String description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// String pronuntiation
        /// </summary>
        public string Pronunciation { get; set; }
    }
}