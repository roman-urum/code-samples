using System;
using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// BriefPatientViewModel.
    /// </summary>
    public class BriefPatientViewModel : BasePatientViewModel
    {
        /// <summary>
        /// List of care managers' ids assigned to patients
        /// </summary>
        public IList<Guid> CareManagers { get; set; }
    }
}