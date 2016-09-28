using System.ComponentModel.DataAnnotations;

namespace HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets
{
    /// <summary>
    /// ScaleAnswerSetUpdateDto.
    /// </summary>
    public class UpdateScaleAnswerSetRequestDto : BaseScaleAnswerSetDto
{
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Required]
        public UpdateScaleAnswerSetLabelsRequestDto Labels { get; set; }
    }
}