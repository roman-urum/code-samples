using System.Collections.Generic;
using System.Linq;

namespace HealthLibrary.Domain.Entities.Program
{
    /// <summary>
    /// Program.
    /// </summary>
    public class Program : CustomerAggregateRoot, ISoftDelitable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public virtual ICollection<Tag> Tags { get; set; }

        /// <summary>
        /// Gets or sets the program elements.
        /// </summary>
        /// <value>
        /// The program elements.
        /// </value>
        public virtual ICollection<ProgramElement> ProgramElements { get; set; }

        /// <summary>
        /// Gets or sets the program day elements.
        /// </summary>
        /// <value>
        /// The program day elements.
        /// </value>
        public virtual ICollection<ProgramDayElement> ProgramDayElements { get; set; }

        /// <summary>
        /// Gets or sets the recurrences.
        /// </summary>
        /// <value>
        /// The recurrences.
        /// </value>
        public virtual ICollection<Recurrence> Recurrences { get; set; }

        ///// <summary>
        ///// Gets or sets the program scores.
        ///// </summary>
        ///// <value>
        ///// The program scores.
        ///// </value>
        //public virtual ICollection<ProgramScore> ProgramScores { get; set; }
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Identifies if program is reccurence.
        /// </summary>
        public bool IsRecurrence
        {
            get { return this.Recurrences.Any(); }
        }
    }
}