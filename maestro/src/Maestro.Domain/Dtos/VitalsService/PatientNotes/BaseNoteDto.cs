using System.Collections.Generic;
using Newtonsoft.Json;

namespace Maestro.Domain.Dtos.VitalsService.PatientNotes
{
    /// <summary>
    /// Class BaseNoteDto.
    /// </summary>
    [JsonObject]
    public class BaseNoteDto
    {
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }      

        /// <summary>
        /// Gets or sets the notables.
        /// </summary>
        /// <value>
        /// The notables.
        /// </value>
        public IList<string> Notables { get; set; }
    }
}