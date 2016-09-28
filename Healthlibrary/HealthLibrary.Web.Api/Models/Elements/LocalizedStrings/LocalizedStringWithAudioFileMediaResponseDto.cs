using HealthLibrary.Web.Api.Models.Elements.Medias;

namespace HealthLibrary.Web.Api.Models.Elements.LocalizedStrings
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