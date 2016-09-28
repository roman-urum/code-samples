using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings
{
    /// <summary>
    /// Model for response with localized string info.
    /// </summary>
    public class LocalizedStringWithAudioFileMediaResponseDto : LocalizedStringResponseDto
    {
        /// <summary>
        /// Data of audio file for localized string.
        /// </summary>
        public MediaResponseDto AudioFileMedia { get; set; }
    }
}