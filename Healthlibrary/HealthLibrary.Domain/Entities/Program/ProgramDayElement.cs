using System;

namespace HealthLibrary.Domain.Entities.Program
{
    /// <summary>
    /// ProgramDayElement.
    /// </summary>
    public class ProgramDayElement : Entity
    {
        /// <summary>
        /// Gets or sets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int? Sort { get; set; }

        /// <summary>
        /// Gets or sets the program identifier.
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public Guid ProgramId { get; set; }
        
        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public virtual Program Program { get; set; }


        /// <summary>
        /// Id to related program element.
        /// </summary>
        public Guid ProgramElementId { get; set; }

        /// <summary>
        /// Reference to related program element.
        /// </summary>
        public virtual ProgramElement ProgramElement { get; set; }

        /// <summary>
        /// Gets or sets the recurrence identifier.
        /// </summary>
        /// <value>
        /// The recurrence identifier.
        /// </value>
        public Guid? RecurrenceId { get; set; }

        /// <summary>
        /// Gets or sets the recurrence.
        /// </summary>
        /// <value>
        /// The recurrence.
        /// </value>
        public virtual Recurrence Recurrence { get; set; }
    }
}