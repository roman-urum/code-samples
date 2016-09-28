using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Web.Security;
using Maestro.Web.Areas.Site.Models.Dashboard;
using System;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IDashboardControllerHelper.
    /// </summary>
    public interface IDashboardControllerHelper
    {
        /// <summary>
        /// Gets the patients cards.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<GetPatientsCardsResultViewModel> GetPatientsCards(GetPatientsCardsRequestViewModel request);

        /// <summary>
        /// Acknowledges alerts.
        /// </summary>
        /// <param name="alertIds">The list if alert identifiers.</param>
        /// <returns></returns>
        Task AcknowledgeAlerts(List<Guid> alertIds);

        /// <summary>
        /// Ignores the reading.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="alertIds">The alerts identifiers.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task IgnoreReading(IMaestroPrincipal user, Guid measurementId, List<Guid> alertIds, Guid patientId);

        /// <summary>
        /// Gets the patient cards.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns></returns>
        Task<IList<FullPatientCardViewModel>> GetPatientCards(GetPatientCardsRequestViewModel request);

        /// <summary>
        /// Clears the cache.
        /// </summary>
        void ClearCache();
    }
}