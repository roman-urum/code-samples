using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// CalendarItemViewModel.
    /// </summary>
    public class CalendarItemViewModel
    {
        /// <summary>
        /// Id of calendar event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of related program.
        /// </summary>
        public Guid? CalendarProgramId { get; set; }

        /// <summary>
        /// Id of program from health library.
        /// </summary>
        public Guid? ProgramId { get; set; }

        /// <summary>
        /// Program name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of day in program.
        /// </summary>
        public int? ProgramDay { get; set; }

        /// <summary>
        /// Gets or sets the due date and time.
        /// </summary>
        /// <value>
        /// The due UTC.
        /// </value>
        public DateTimeOffset? Due { get; set; }

        /// <summary>
        /// Key to combine events from the same program.
        /// </summary>
        public Guid? Correlator { get; set; }

        /// <summary>
        /// Gets or sets the expire minutes.
        /// </summary>
        /// <value>
        /// The expire minutes.
        /// </value>
        public int? ExpireMinutes { get; set; }

        /// <summary>
        /// Gets or sets the protocols.
        /// </summary>
        /// <value>
        /// The protocols.
        /// </value>
        public IList<ProtocolElementViewModel> Protocols { get; set; }

        /// <summary>
        /// Gets or sets the reccurence rules (for reccuring calendar items only).
        /// </summary>
        /// <value>
        /// The reccurence rules.
        /// </value>
        public IList<RecurrenceRuleViewModel> RecurrenceRules { get; set; }
    }
}