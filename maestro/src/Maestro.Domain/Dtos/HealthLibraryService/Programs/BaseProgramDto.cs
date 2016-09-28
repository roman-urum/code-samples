using System.Collections.Generic;

namespace Maestro.Domain.Dtos.HealthLibraryService.Programs
{
    public class BaseProgramDto
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
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the recurrences.
        /// </summary>
        /// <value>
        /// The recurrences.
        /// </value>
        public List<RecurrenceDto> Recurrences { get; set; }
    }
}
