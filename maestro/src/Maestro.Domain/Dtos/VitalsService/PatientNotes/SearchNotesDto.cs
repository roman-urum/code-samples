using System;
using System.Collections.Generic;

namespace Maestro.Domain.Dtos.VitalsService.PatientNotes
{
    /// <summary>
    /// SearchNotesDto.
    /// </summary>
    public class SearchNotesDto : BaseSearchDto
    {
        /// <summary>
        /// The customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The patient identifier.
        /// </summary>
        public Guid PatientId { get; set; }

        /// <summary>
        /// The list of notables.
        /// </summary>
        public IList<string> Notables { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is brief or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is brief; otherwise, <c>false</c>.
        /// </value>
        public bool IsBrief { get; set; }
    }
}