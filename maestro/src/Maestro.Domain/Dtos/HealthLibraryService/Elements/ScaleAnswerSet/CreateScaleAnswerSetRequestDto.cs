using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet
{
    /// <summary>
    /// CreateScaleAnswerSetRequestDto.
    /// </summary>
    [JsonObject]
    public class CreateScaleAnswerSetRequestDto : BaseScaleAnswerSetDto
    {
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public CreateScaleAnswerSetLabelsRequestDto Labels { get; set; }
    }
}