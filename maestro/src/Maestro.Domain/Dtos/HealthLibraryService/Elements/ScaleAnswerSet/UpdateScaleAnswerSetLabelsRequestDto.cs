using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet
{
    /// <summary>
    /// ScaleAnswerSetLabelsUpdateDto.
    /// </summary>
    [JsonObject]
    public class UpdateScaleAnswerSetLabelsRequestDto
    {
        /// <summary>
        /// Gets or sets the low label localized.
        /// </summary>
        /// <value>
        /// The low label localized.
        /// </value>
        public UpdateLocalizedStringRequestDto LowLabel { get; set; }
        /// <summary>
        /// Gets or sets the mid label localized.
        /// </summary>
        /// <value>
        /// The mid label localized.
        /// </value>
        public UpdateLocalizedStringRequestDto MidLabel { get; set; }
        /// <summary>
        /// Gets or sets the highd label localized.
        /// </summary>
        /// <value>
        /// The highd label localized.
        /// </value>
        public UpdateLocalizedStringRequestDto HighLabel { get; set; }
    }
}