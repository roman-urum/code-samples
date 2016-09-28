using System;

namespace VitalsService.Domain.DbEntities
{
    /// <summary>
    /// NoteNotable.
    /// </summary>
    public class NoteNotable : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public Guid NoteId { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public virtual Note Note { get; set; }
    }
}