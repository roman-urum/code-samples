using System;

namespace HealthLibrary.Domain
{
    /// <summary>
    /// Provide a contract for all entities that should be audited.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated UTC.
        /// </summary>
        /// <value>
        /// The updated UTC.
        /// </value>
        DateTime UpdatedUtc { get; set; }
    }
}