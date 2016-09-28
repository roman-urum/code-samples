using System.Collections.Generic;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet
{
    /// <summary>
    /// Contains base properties for Selection answer set.
    /// </summary>
    public abstract class BaseSelectionAnswerSetDto
    {
        /// <summary>
        /// Name of new answerset.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of answer selection.
        /// </summary>
        public bool IsMultipleChoice { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        public List<string> Tags { get; set; }
    }
}