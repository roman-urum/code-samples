using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.DomainObjects;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IAlertsService.
    /// </summary>
    public interface IAlertsService
    {
        /// <summary>
        /// Creates the alert.
        /// </summary>
        /// <param name="alert">The alert.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateAlertStatus>> CreateAlert(Alert alert);

        /// <summary>
        /// Acknowledges the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="acknowledgedBy">The acknowledged by.</param>
        /// <param name="alertsIds">The alerts ids.</param>
        /// <returns></returns>
        Task<CreateUpdateAlertStatus> AcknowledgeAlerts(int customerId, Guid acknowledgedBy, IList<Guid> alertsIds);

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        PagedResult<PatientAlerts> GetAlerts(int customerId, AlertsSearchDto request);

        /// <summary>
        /// Create alerts assign them to to measurement.Vitals in case of vital violation
        /// </summary>
        /// <param name="measurement">The measurement</param>
        /// <param name="aggregatedThresholds">The aggregated thresholds</param>
        /// <returns></returns>
        void CreateViolationAlerts(Measurement measurement, IList<Threshold> aggregatedThresholds);
    }
}