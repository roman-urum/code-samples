using System;
using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// CalendarItemsSearchDto.
    /// </summary>
    public class CalendarItemsSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarItemsSearchDto"/> class.
        /// </summary>
        public CalendarItemsSearchDto()
        {
            var now = DateTime.UtcNow;

            ScheduledAfter = now.AddDays(-30);
            ScheduledBefore = now;
        }

        /// <summary>
        /// Gets or sets the scheduled after.
        /// </summary>
        /// <value>
        /// The scheduled after.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? ScheduledAfter { get; set; }

        /// <summary>
        /// Gets or sets the scheduled before.
        /// </summary>
        /// <value>
        /// The scheduled before.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime? ScheduledBefore { get; set; }

        /// <summary>
        /// Filters calendar items by correlator.
        /// </summary>
        [RequestParameter(RequestParameterType.QueryString)]
        public Guid? Correlator { get; set; }
    }
}