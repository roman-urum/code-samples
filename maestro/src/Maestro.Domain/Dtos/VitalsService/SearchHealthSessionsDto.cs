using System;
using Maestro.Common.ApiClient;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Domain.Dtos.VitalsService.Enums.Ordering;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// SearchHealthSessionsDto.
    /// </summary>
    public class SearchHealthSessionsDto : OrderedSearchDto<HealthSessionOrderBy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchHealthSessionsDto"/> class.
        /// </summary>
        public SearchHealthSessionsDto()
        {
            var now = DateTime.UtcNow;

            StartedFromUtc = now.AddDays(-30);
            CompletedToUtc = now;
        }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the started from UTC.
        /// </summary>
        /// <value>
        /// The started from UTC.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? StartedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the started to UTC.
        /// </summary>
        /// <value>
        /// The started to UTC.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? StartedToUtc { get; set; }

        /// <summary>
        /// Gets or sets the completed from UTC.
        /// </summary>
        /// <value>
        /// The completed from UTC.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? CompletedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the completed to UTC.
        /// </summary>
        /// <value>
        /// The completed to UTC.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? CompletedToUtc { get; set; }

        /// <summary>
        /// Gets or sets the submitted from UTC.
        /// </summary>
        /// <value>
        /// The submitted from UTC.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? SubmittedFromUtc { get; set; }

        /// <summary>
        /// Gets or sets the submitted to UTC.
        /// </summary>
        /// <value>
        /// The submitted to UTC.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? SubmittedToUtc { get; set; }

        /// <summary>
        /// Gets or sets the health session element type
        /// </summary>
        /// <value>
        /// The health session element type
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public HealthSessionElementType? ElementType { get; set; }

        /// <summary>
        /// Gets or sets the calendar item identifier.
        /// </summary>
        /// <value>
        /// The calendar item identifier.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? CalendarItemId { get; set; }
    }
}
