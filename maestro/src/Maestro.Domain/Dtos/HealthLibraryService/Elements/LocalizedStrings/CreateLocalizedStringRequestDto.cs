using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings
{
    /// <summary>
    /// Model for localized string.
    /// </summary>
    public class CreateLocalizedStringRequestDto : BaseLocalizedStringDto
    {
        /// <summary>
        /// Optional audio prompt in base64 format.
        /// </summary>
        public CreateMediaRequestDto AudioFileMedia { get; set; }
    }
}