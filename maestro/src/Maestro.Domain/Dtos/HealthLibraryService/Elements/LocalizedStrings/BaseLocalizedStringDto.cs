namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings
{
    /// <summary>
    /// Base model for localized string.
    /// </summary>
    public class BaseLocalizedStringDto
    {
        /// <summary>
        /// Translate.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// String description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// String pronuntiation
        /// </summary>
        public string Pronunciation { get; set; }
    }
}