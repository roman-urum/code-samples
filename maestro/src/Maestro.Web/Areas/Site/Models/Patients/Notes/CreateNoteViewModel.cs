using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Site.Models.Patients.Notes
{
    /// <summary>
    /// The CreateNoteViewMovel class.
    /// </summary>
    public class CreateNoteViewModel
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Required(ErrorMessage = null, 
                  ErrorMessageResourceName = "Note_PatientIdError",
                  ErrorMessageResourceType = typeof(GlobalStrings))]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [Required(ErrorMessage = null,
          ErrorMessageResourceName = "Note_TextError",
          ErrorMessageResourceType = typeof(GlobalStrings))]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the list of notables.
        /// </summary>
        /// <value>
        /// The list of notables.
        /// </value>
        public List<string> Notables { get; set; }

        /// <summary>
        /// Gets or sets the vital identifier.
        /// </summary>
        /// <value>
        /// The vital identifier.
        /// </value>
        public Guid? MeasurementId { get; set; }

        /// <summary>
        /// Gets or sets the health session element identifier.
        /// </summary>
        /// <value>
        /// The health session element identifier.
        /// </value>
        public Guid? HealthSessionElementId { get; set; }
    }
}