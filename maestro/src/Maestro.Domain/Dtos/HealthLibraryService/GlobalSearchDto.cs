using System.Collections.Generic;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Domain.Dtos.HealthLibraryService
{
    /// <summary>
    /// GlobalSearchDto.
    /// </summary>
    public class GlobalSearchDto : TagsSearchDto
    {
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public IList<SearchCategoryType> Categories { get; set; }
    }
}