namespace HealthLibrary.Web.Api.Models.Elements.LocalizedStrings
{
    /// <summary>
    /// Model for response with localized string info.
    /// </summary>
    public class LocalizedStringResponseDto : BaseLocalizedStringDto
    {
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }    
    }
}