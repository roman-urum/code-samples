using System.Collections.Generic;

namespace HealthLibrary.Domain.Dtos
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
        public IList<string> Tags { get; set; }
    }
}