using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Domain.Dtos.PatientsService.Calendar
{
    /// <summary>
    /// RecurrenceRuleDto.
    /// </summary>
    public class RecurrenceRuleDto
    {
        /// <summary>
        /// Gets or sets the start date UTC.
        /// </summary>
        /// <value>
        /// The start date UTC.
        /// </value>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the end date UTC.
        /// </summary>
        /// <value>
        /// The end date UTC.
        /// </value>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the frequency (Daily, Weekly, Monthly).
        /// </summary>
        /// <value>
        /// The frequency.
        /// </value>
        public Frequency? Frequency { get; set; }

        /// <summary>
        /// interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public int? Interval { get; set; }

        /// <summary>
        /// Gets or sets the week days.
        /// </summary>
        /// <value>
        /// The week days.
        /// </value>
        public IList<DayOfWeek> WeekDays { get; set; }

        /// <summary>
        /// Gets or sets the month days.
        /// </summary>
        /// <value>
        /// The month days.
        /// </value>
        public IList<int> MonthDays { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int? Count { get; set; }
    }
}
