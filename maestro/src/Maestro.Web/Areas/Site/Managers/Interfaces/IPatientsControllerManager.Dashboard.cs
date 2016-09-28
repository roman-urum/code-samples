using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Web.Areas.Site.Models;
using Maestro.Web.Areas.Site.Models.Patients.Dashboard;
using Maestro.Web.Areas.Site.Models.Patients.DetailedData;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.Dashboard
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the patient specific thresholds.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<PatientSpecificThresholdsViewModel> GetPatientSpecificThresholds(Guid patientId);

        /// <summary>
        /// Gets the peripherals.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<IList<PatientPeripheralViewModel>> GetPeripherals(Guid patientId);

        /// <summary>
        /// Gets the health sessions dashboard.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<PatientHealthSessionsDashboardViewModel> GetHealthSessionsDashboard(Guid patientId);

        /// <summary>
        /// Gets the latest information dashboard.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<PatientLatestInformationDashboardViewModel> GetLatestInformationDashboard(Guid patientId);

        /// <summary>
        /// Gets the grouped health sessions detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        Task<IList<HealthSessionDetailedDataGroupViewModel>> GetGroupedHealthSessionsDetailedData(
            SearchDetailedDataViewModel searchModel
        );

        /// <summary>
        /// Gets the ungrouped health sessions detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        Task<IList<HealthSessionDetailedDataGroupElementViewModel>> GetUngroupedHealthSessionsDetailedData(
            SearchUngroupedHealthSessionDetailedDataViewModel searchModel
        );

        /// <summary>
        /// Gets the adhoc measurements detailed data.
        /// </summary>
        /// <param name="searchModel">The search model.</param>
        /// <returns></returns>
        Task<IList<MeasurementViewModel>> GetAdhocMeasurementsDetailedData(
            SearchAdhocMeasurementsDetailedDataViewModel searchModel
        );

        /// <summary>
        /// Exports the detailed data to excel.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="groupByHealthSession"></param>
        /// <param name="observedFromUtc">The observed from UTC.</param>
        /// <param name="observedToUtc">The observed to UTC.</param>
        /// <param name="elementType">Type of the element.</param>
        /// <param name="vitalType">Type of the vital.</param>
        /// <param name="questionId">The question identifier.</param>
        /// <returns></returns>
        Task<byte[]> ExportDetailedDataToExcel(
            Guid patientId, 
            bool groupByHealthSession, 
            DateTime? observedFromUtc = null,
            DateTime? observedToUtc = null, 
            HealthSessionElementType? elementType = null, 
            VitalType? vitalType = null,
            Guid? questionId = null
        );
    }
}