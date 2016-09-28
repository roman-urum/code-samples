using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Maestro.Domain.Dtos.PatientsService.Enums;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// RecurrenceRuleViewModel.
    /// </summary>
    public class RecurrenceRuleViewModel
    {
        /// <summary>
        /// Gets or sets the start date in patient's time zone.
        /// </summary>
        /// <value>
        /// The start date in patient's time zone.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date in patient's time zone.
        /// </summary>
        /// <value>
        /// The end date in patient's time zone.
        /// </value>
        public DateTime? EndDate { get; set; }

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