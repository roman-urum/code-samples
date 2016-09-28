using System;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// ProgramResponseDto.
    /// </summary>
    public class ProgramBriefResponseDto : ProgramRequestDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Identifies if program is requrrence.
        /// </summary>
        public bool IsRecurrence { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated UTC.
        /// </summary>
        /// <value>
        /// The updated UTC.
        /// </value>
        public DateTime UpdatedUtc { get; set; }
    }
}