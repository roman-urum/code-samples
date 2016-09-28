using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Web.Api.Models.Elements.AssessmentElements
{
    /// <summary>
    /// AssessmentResponseDto.
    /// </summary>
    public class AssessmentResponseDto : ElementDto
    {
        /// <summary>
        /// Gets or sets the type of the Assessment.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public AssessmentType AssessmentType { get; set; }

        /// <summary>
        /// Gets or sets the name of Assessment.
        /// </summary>
        public string Name { get; set; }
    }
}