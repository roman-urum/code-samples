using System;
using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// Search criteria to generate program schedule.
    /// </summary>
    public class ProgramScheduleRequestDto
    {
        /// <summary>
        /// The calendar date that represents the first day of a program.
        /// </summary>
        [RequestParameter(RequestParameterType.QueryString)]
        public DateTime StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the start date timezone.
        /// </summary>
        /// <value>
        /// The start date timezone.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public string StartDateTz { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int? StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int? EndDay { get; set; }

        /// <summary>
        /// The number of minutes after which the calendar event should expire. 
        /// Optional. Default is null.
        /// </summary>
        [RequestParameter(RequestParameterType.QueryString)]
        public int? ExpireMinutes { get; set; }
    }
}
