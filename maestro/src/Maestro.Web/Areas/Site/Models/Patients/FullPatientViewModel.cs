using System.Collections.Generic;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// FullPatientViewModel.
    /// </summary>
    public class FullPatientViewModel : BasePatientViewModel
    {
        /// <summary>
        /// List of care managers' ids assigned to patients
        /// </summary>
        public IList<CareManagerViewModel> CareManagers { get; set; }
    }
}