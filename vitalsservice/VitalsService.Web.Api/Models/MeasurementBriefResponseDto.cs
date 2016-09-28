using System;
using System.Collections.Generic;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// MeasurementBriefResponseDto.
    /// </summary>
    public class MeasurementBriefResponseDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        public DateTime UpdatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public DateTime ObservedUtc { get; set; }

        /// <summary>
        /// Gets or sets the observed tz.
        /// </summary>
        /// <value>
        /// The observed tz.
        /// </value>
        public string ObservedTz { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MeasurementBriefResponseDto"/> is invalidated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if invalidated; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalidated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MeasurementBriefResponseDto"/> is automated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutomated { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the vitals.
        /// </summary>
        /// <value>
        /// The vitals.
        /// </value>
        public IList<VitalBriefResponseDto> Vitals { get; set; }
    }
}