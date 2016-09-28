using System;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// SearchVitalsDto.
    /// </summary>
    public class SearchVitalsDto : BaseSearchDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchVitalsDto"/> class.
        /// </summary>
        public SearchVitalsDto()
        {
            var now = DateTime.UtcNow;

            ObservedFrom = now.AddDays(-30);
            ObservedTo = now;
        }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the observed from.
        /// </summary>
        /// <value>
        /// The observed from.
        /// </value>
        public DateTime? ObservedFrom { get; set; }

        /// <summary>
        /// Gets or sets the observed to.
        /// </summary>
        /// <value>
        /// The observed to.
        /// </value>
        public DateTime? ObservedTo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is invalidated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is invalidated; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalidated { get; set; }
    }
}