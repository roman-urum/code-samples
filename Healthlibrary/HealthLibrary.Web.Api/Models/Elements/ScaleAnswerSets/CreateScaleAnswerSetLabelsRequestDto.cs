using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets
{
    /// <summary>
    /// ScaleAnswerSetLabelsDto.
    /// </summary>
    public class CreateScaleAnswerSetLabelsRequestDto
    {
        /// <summary>
        /// Gets or sets the low label localized.
        /// </summary>
        /// <value>
        /// The low label localized.
        /// </value>
        [Required]
        public CreateLocalizedStringRequestDto LowLabel { get; set; }
        /// <summary>
        /// Gets or sets the mid label localized.
        /// </summary>
        /// <value>
        /// The mid label localized.
        /// </value>
        public CreateLocalizedStringRequestDto MidLabel { get; set; }
        /// <summary>
        /// Gets or sets the highd label localized.
        /// </summary>
        /// <value>
        /// The highd label localized.
        /// </value>
        [Required]
        public CreateLocalizedStringRequestDto HighLabel { get; set; }
    }
}