using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets
{
    /// <summary>
    /// ScaleAnswerSetResponseDto.
    /// </summary>
    public class ScaleAnswerSetResponseDto : OpenEndedAnswerSetResponseDto
    {
        /// <summary>
        /// Gets or sets the low label.
        /// </summary>
        public LocalizedStringWithAudioFileMediaResponseDto LowLabel { get; set; }
        
        /// <summary>
        /// Gets or sets the mid label.
        /// </summary>
        public LocalizedStringWithAudioFileMediaResponseDto MidLabel { get; set; }
        
        /// <summary>
        /// Gets or sets the high label.
        /// </summary>
        public LocalizedStringWithAudioFileMediaResponseDto HighLabel { get; set; }

        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>
        /// The low value.
        /// </value>
        public int LowValue { get; set; }

        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>
        /// The high value.
        /// </value>
        public int HighValue { get; set; }
    }
}