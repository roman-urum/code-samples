using System;

namespace HealthLibrary.Domain.Dtos
{
    /// <summary>
    /// SearchProgramDto.
    /// </summary>
    public class SearchProtocolDto : TagsSearchDto
    {
        /// <summary>
        /// Gets or sets the created after value.
        /// </summary>
        /// <value>
        /// The created after value.
        /// </value>
        public DateTime? CreatedAfter { get; set; }

        /// <summary>
        /// Gets or sets the updated after value.
        /// </summary>
        /// <value>
        /// The updated after value.
        /// </value>
        public DateTime? UpdatedAfter { get; set; }

        /// <summary>
        /// Gets or sets the created before value.
        /// </summary>
        /// <value>
        /// The created before value.
        /// </value>
        public DateTime? CreatedBefore { get; set; }

        /// <summary>
        /// Gets or sets the updated before value.
        /// </summary>
        /// <value>
        /// The updated before value.
        /// </value>
        public DateTime? UpdatedBefore { get; set; }
    }
}