using System;
using VitalsService.CustomAttributes;
using VitalsService.Domain.Enums;
using VitalsService.Domain.Enums.Ordering;
using VitalsService.Domain.Resources;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// Model for health sessions search criteria.
    /// </summary>
    public class HealthSessionsSearchDto : OrderedSearchDto<HealthSessionOrderBy>
    {
        /// <summary>
        /// Gets or sets the started from UTC.
        /// </summary>
        /// <value>
        /// The started from UTC.
        /// </value>
        public DateTime? StartedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the started to UTC.
        /// </summary>
        /// <value>
        /// The started to UTC.
        /// </value>
        [GreaterThan(
            "StartedFromUtc",
            PassOnNull = true,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? StartedToUtc { get; set; }

        /// <summary>
        /// Gets or sets the completed from UTC.
        /// </summary>
        /// <value>
        /// The completed from UTC.
        /// </value>
        public DateTime? CompletedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the completed to UTC.
        /// </summary>
        /// <value>
        /// The completed to UTC.
        /// </value>
        [GreaterThan(
            "CompletedFromUtc",
            PassOnNull = true,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? CompletedToUtc { get; set; }

        /// <summary>
        /// Gets or sets the submitted from UTC.
        /// </summary>
        /// <value>
        /// The submitted from UTC.
        /// </value>
        public DateTime? SubmittedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the submitted to UTC.
        /// </summary>
        /// <value>
        /// The submitted to UTC.
        /// </value>
        [GreaterThan(
            "SubmittedFromUtc",
            PassOnNull = true,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? SubmittedToUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include private].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [include private]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludePrivate { get; set; }

        /// <summary>
        /// Gets or sets the calendar item identifier.
        /// </summary>
        /// <value>
        /// The calendar item identifier.
        /// </value>
        public Guid? CalendarItemId { get; set; }

        /// <summary>
        /// Gets or sets the type of the element.
        /// </summary>
        /// <value>
        /// The type of the element.
        /// </value>
        public HealthSessionElementType? ElementType { get; set; }
    }
}