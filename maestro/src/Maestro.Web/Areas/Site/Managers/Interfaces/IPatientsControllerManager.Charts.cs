using System;
using System.Threading.Tasks;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.CareManagers
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the vitals chart.
        /// </summary>
        /// <param name="getChartRequest">The get chart request.</param>
        /// <returns></returns>
        Task<VitalsChartViewModel> GetVitalsChart(GetVitalsChartViewModel getChartRequest);

        /// <summary>
        /// Gets the assessment chart.
        /// </summary>
        /// <param name="getChartRequest">The get chart request.</param>
        /// <returns></returns>
        Task<AssessmentChartViewModel> GetAssessmentChart(GetAssessmentChartViewModel getChartRequest);

        /// <summary>
        /// Generates settings entities using model data.
        /// Saves settings in db using service.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task SaveTrendsSettings(Guid patientId, TrendsSettingsViewModel model);

        /// <summary>
        /// Returns settings of chards for specified patient.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<TrendsSettingsViewModel> GetTrendsSettings(Guid patientId);

        /// <summary>
        /// Exports the trends to excel.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<Tuple<string, byte[]>> ExportTrendsToExcel(Guid patientId);
    }
}