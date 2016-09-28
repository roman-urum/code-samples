using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Maestro.Domain.Dtos.PatientsService;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.CareManagers;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.CareManagers
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the available care managers.
        /// </summary>
        /// <returns></returns>
        Task<IList<CareManagerViewModel>> GetAvailableCareManagers(SearchCareManagersViewModel searchRequest = null);
    }
}