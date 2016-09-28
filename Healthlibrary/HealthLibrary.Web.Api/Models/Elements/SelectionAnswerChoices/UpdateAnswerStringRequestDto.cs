using System;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// UpdateAnswerStringRequestDto.
    /// </summary>
    public class UpdateAnswerStringRequestDto : BaseAnswerStringRequestDto
    {
        /// <summary>
        /// Id of existing media which already was uploaded.
        /// </summary>
        public Guid? AudioFileMediaId { get; set; }

        /// <summary>
        /// Optional audio prompt in base64 format.
        /// </summary>
        [MediaExtension(
            ".mp3", ".wav",
            ErrorMessageResourceName = "InvalidAudioExtension_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings)
        )]
        public UpdateMediaRequestDto AudioFileMedia { get; set; }
    }
}