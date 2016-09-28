using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Alerts;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IAlertsControllerHelper.
    /// </summary>
    public interface IAlertsControllerHelper
    {
        /// <summary>
        /// Creates the alert.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateAlertStatus>> CreateAlert(int customerId, AlertRequestDto request);

        /// <summary>
        /// Acknowledges the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateUpdateAlertStatus> AcknowledgeAlerts(int customerId, AcknowledgeAlertsRequestDto request);

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        PagedResultDto<PatientAlertsDto> GetAlerts(int customerId, AlertsSearchDto request);
    }
}