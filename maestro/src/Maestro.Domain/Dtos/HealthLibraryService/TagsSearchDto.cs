using System.Collections.Generic;
using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.HealthLibraryService
{
    /// <summary>
    /// TagsSearchDto.
    /// </summary>
    public class TagsSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public IList<string> Tags { get; set; }
    }
}