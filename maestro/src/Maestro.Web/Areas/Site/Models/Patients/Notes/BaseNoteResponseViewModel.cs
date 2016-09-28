using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.Notes
{
    /// <summary>
    /// The base view model for note response. 
    /// </summary>
    public class BaseNoteResponseViewModel
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created UTC.
        /// </summary>
        /// <value>
        /// The created UTC.
        /// </value>
        public DateTime CreatedUtc { get; set; }

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