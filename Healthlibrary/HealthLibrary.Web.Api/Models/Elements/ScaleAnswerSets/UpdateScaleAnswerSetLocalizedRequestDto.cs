using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets
{
    /// <summary>
    /// ScaleAnswerSetLocalizedDto.
    /// </summary>
    public class UpdateScaleAnswerSetLocalizedRequestDto
    {
        /// <summary>
        /// Gets or sets the low label.
        /// </summary>
        [Required]
        public UpdateLocalizedStringRequestDto LowLabel { get; set; }

        /// <summary>
        /// Gets or sets the mid label. (optional field)
        /// </summary>
        public UpdateLocalizedStringRequestDto MidLabel { get; set; }

        /// <summary>
        /// Gets or sets the high label.
        /// </summary>
        [Required]
        public UpdateLocalizedStringRequestDto HighLabel { get; set; }
    }
}