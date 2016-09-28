using System.ComponentModel.DataAnnotations;

namespace HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets
{
    /// <summary>
    /// CreateScaleAnswerSetRequestDto.
    /// </summary>
    public class CreateScaleAnswerSetRequestDto : BaseScaleAnswerSetDto
    {
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Required]
        public CreateScaleAnswerSetLabelsRequestDto Labels { get; set; }
    }
}