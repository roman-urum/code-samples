using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Maestro.Domain.Dtos.PatientsService;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    public class CareManagerWithPatientsDetailsViewModel: CareManagerViewModel
    {
        /// <summary>
        /// Gets or sets the list of patient's statuses assigned to this care manager.
        /// </summary>
        /// <value>
        /// The list of patient's statuses assigned to this care manager.
        /// </value>
        public List<PatientStatus> AssignedPatientStatuses { get; set; }
    }
}