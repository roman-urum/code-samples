using System;
using System.Runtime.Serialization;

namespace Maestro.Domain.Dtos.VitalsService.PatientNotes
{
    /// <summary>
    /// Base model for note response models
    /// </summary>
    [KnownType(typeof(NoteBriefResponseDto))]
    [KnownType(typeof(NoteDetailedResponseDto))]
    public class BaseNoteResponseDto : BaseNoteDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }
    }
}