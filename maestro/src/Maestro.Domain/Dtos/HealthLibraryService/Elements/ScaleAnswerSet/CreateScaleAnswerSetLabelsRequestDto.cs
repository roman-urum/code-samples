using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet
{
    /// <summary>
    /// ScaleAnswerSetLabelsDto.
    /// </summary>
    [JsonObject]
    public class CreateScaleAnswerSetLabelsRequestDto
    {
        /// <summary>
        /// Gets or sets the low label localized.
        /// </summary>
        /// <value>
        /// The low label localized.
        /// </value>
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
        public CreateLocalizedStringRequestDto HighLabel { get; set; }
    }
}