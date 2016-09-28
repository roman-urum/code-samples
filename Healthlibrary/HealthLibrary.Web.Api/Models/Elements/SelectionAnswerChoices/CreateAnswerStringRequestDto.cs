using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// CreateAnswerStringRequestDto.
    /// </summary>
    public class CreateAnswerStringRequestDto : BaseAnswerStringRequestDto
    {
        /// <summary>
        /// Gets or sets the audio file media.
        /// </summary>
        /// <value>
        /// The audio file media.
        /// </value>
        [MediaExtension(
            ".mp3", ".wav",
            ErrorMessageResourceName = "InvalidAudioExtension_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings)
        )]
        public CreateMediaRequestDto AudioFileMedia { get; set; }
    }
}