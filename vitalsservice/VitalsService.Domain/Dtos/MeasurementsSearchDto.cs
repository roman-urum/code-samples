using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// MeasurementsSearchDto.
    /// </summary>
    public class MeasurementsSearchDto : BaseSearchDto
    {
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
        [GreaterThan(
            "ObservedFrom",
            PassOnNull = true,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? ObservedTo { get; set; }

        /// <summary>
        /// Gets or sets the is invalidated.
        /// </summary>
        /// <value>
        /// The is invalidated.
        /// </value>
        public bool? IsInvalidated { get; set; }

        /// <summary>
        /// Gets or sets the Observed time zone.
        /// </summary>
        /// <value>
        /// The Observed time zone.
        /// </value>
        public string ObservedTz { get; set; }
    }
}