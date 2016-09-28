using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Web.Areas.Site.Models.Patients;
using Maestro.Web.Areas.Site.Models.Patients.Vitals;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.CareManagers
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Searches the vitals.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        Task<PagedResult<MeasurementDto>> SearchVitals(SearchVitalViewModel searchModel);

        /// <summary>
        /// Invalidates measurement
        /// </summary>
        /// <param name="request">The invalidate emasurement request</param>
        /// <returns></returns>
        Task InvalidateMeasurement(InvalidateMeasurementViewModel request);
    }
}