using System;
using System.Collections.Generic;

namespace HealthLibrary.Domain.Entities.Program
{
    /// <summary>
    /// Recurrence.
    /// </summary>
    public class Recurrence : Entity
    {
        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        public int EndDay { get; set; }

        /// <summary>
        /// Gets or sets the every days.
        /// </summary>
        /// <value>
        /// The every days.
        /// </value>
        public int EveryDays { get; set; }

        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public virtual Guid ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public virtual Program Program { get; set; }

        /// <summary>
        /// Gets or sets the program day elements.
        /// </summary>
        /// <value>
        /// The program day elements.
        /// </value>
        public virtual ICollection<ProgramDayElement> ProgramDayElements { get; set; }
    }
}