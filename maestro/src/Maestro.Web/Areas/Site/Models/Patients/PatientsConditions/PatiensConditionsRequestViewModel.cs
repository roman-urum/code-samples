using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Maestro.Web.DataAnnotations;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Site.Models.Patients.PatientsConditions
{
    /// <summary>
    /// PatiensConditionsRequestViewModel.
    /// </summary>
    public class PatiensConditionsRequestViewModel
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [Required(
            ErrorMessage = null,
            ErrorMessageResourceName = "PatientConditions_PatientIdRequiredMessage",
            ErrorMessageResourceType = typeof(GlobalStrings))]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or sets the list of conditions identifiers.
        /// </summary>
        /// <value>
        /// The list of conditions identifiers.
        /// </value>
        [NotEmptyList(
            ErrorMessage = null,
            ErrorMessageResourceName = "PatientConditions_PatientConditionsIdsRequiredMessage",
            ErrorMessageResourceType = typeof(GlobalStrings))]
        public IList<Guid> PatientConditionsIds { get; set; }
    }
}