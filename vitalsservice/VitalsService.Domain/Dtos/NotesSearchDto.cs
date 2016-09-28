using System.Collections.Generic;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// NotesSearchDto.
    /// </summary>
    public class NotesSearchDto : BaseSearchDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotesSearchDto"/> class.
        /// </summary>
        public NotesSearchDto()
        {
            IsBrief = true;
        }

        /// <summary>
        /// Gets or sets the notables.
        /// </summary>
        /// <value>
        /// The notables.
        /// </value>
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