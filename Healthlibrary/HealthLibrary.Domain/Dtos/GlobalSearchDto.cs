using System.Collections.Generic;
using HealthLibrary.Domain.Dtos.Enums;

namespace HealthLibrary.Domain.Dtos
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
        public IList<SearchCategoryType> Categories { get; set; }
    }
}