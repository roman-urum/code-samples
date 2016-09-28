using System;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings
{
    /// <summary>
    /// Model for requests to update localized string model entity.
    /// </summary>
    public class UpdateLocalizedStringRequestDto : BaseLocalizedStringDto
    {
        /// <summary>
        /// Id of existing media which already was uploaded.
        /// </summary>
        public Guid? AudioFileMediaId { get; set; }

        /// <summary>
        /// Optional audio prompt in base64 format.
        /// </summary>
        public UpdateMediaRequestDto AudioFileMedia { get; set; }
    }
}