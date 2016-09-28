using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements
{
    /// <summary>
    /// Base model for search requests.
    /// </summary>
    public class SearchRequestDto : TagsSearchDto
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [RequestParameter(RequestParameterType.UrlSegment)]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }
    }
}