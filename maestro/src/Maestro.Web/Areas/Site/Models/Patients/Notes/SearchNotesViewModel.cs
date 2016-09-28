using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Maestro.Domain.Dtos;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Site.Models.Patients.Notes
{
    /// <summary>
    /// SearchNotesViewModel.
    /// </summary>
    public class SearchNotesViewModel:BaseSearchDto
    {
        /// <summary>
        /// Gets or sets the patient identifier
        /// </summary>
        [Required(ErrorMessage = null,
                  ErrorMessageResourceName = "PatientIdentifierDefaultValidationErrorMessage",
                  ErrorMessageResourceType = typeof(GlobalStrings))]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the list of notables
        /// </summary>
        public List<string> Notables { get; set; }
    }
}