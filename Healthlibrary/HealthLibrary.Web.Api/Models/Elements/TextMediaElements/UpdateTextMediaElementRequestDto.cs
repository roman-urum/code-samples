using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;

namespace HealthLibrary.Web.Api.Models.Elements.TextMediaElements
{
    /// <summary>
    /// UpdateTextMediaElementRequestDto.
    /// </summary>
    public class UpdateTextMediaElementRequestDto : CreateTextMediaElementRequestDto
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public new UpdateLocalizedStringRequestDto Text { get; set; }
    }
}