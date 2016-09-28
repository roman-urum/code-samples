using System.Collections.Generic;
using HealthLibrary.Domain.Entities.Enums;

namespace HealthLibrary.Domain.Dtos
{
    /// <summary>
    /// MediaSearchDto.
    /// </summary>
    public class MediaSearchDto : TagsSearchDto
    {
        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>
        /// The types.
        /// </value>
        public IList<MediaType> Types { get; set; }
    }
}