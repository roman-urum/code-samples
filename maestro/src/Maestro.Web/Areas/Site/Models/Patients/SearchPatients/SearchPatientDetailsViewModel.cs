using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients.SearchPatients
{
    /// <summary>
    /// SearchPatientDetails represents the details of patient which was found.
    /// </summary>
    public class SearchPatientDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>The patient identifier.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the list of patient's programs.
        /// </summary>
        /// <value>The list of patient's programs.</value>
        public List<PatientProgramViewModel> Programs { get; set; }
    }
}