using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.DomainObjects;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Alerts;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// AlertsControllerHelper.
    /// </summary>
    public class AlertsControllerHelper : IAlertsControllerHelper
    {
        private readonly IAlertsService alertsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertsControllerHelper"/> class.
        /// </summary>
        /// <param name="alertsService">The alerts service.</param>
        public AlertsControllerHelper(IAlertsService alertsService)
        {
            this.alertsService = alertsService;
        }

        /// <summary>
        /// Creates the alert.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateUpdateAlertStatus>> CreateAlert(
            int customerId,
            AlertRequestDto request
        )
        {
            var alert = Mapper.Map<AlertRequestDto, Alert>(request);
            alert.CustomerId = customerId;

            return await alertsService.CreateAlert(alert);
        }

        /// <summary>
        /// Acknowledges the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CreateUpdateAlertStatus> AcknowledgeAlerts(
            int customerId,
            AcknowledgeAlertsRequestDto request
        )
        {
            var distinctAlertsIds = request.AlertIds.Distinct().ToList();

            return await alertsService.AcknowledgeAlerts(
                customerId, 
                request.AcknowledgedBy ?? Guid.Empty,
                distinctAlertsIds
            );
        }

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public PagedResultDto<PatientAlertsDto> GetAlerts(int customerId, AlertsSearchDto request)
        {
            if (request != null && request.Take <= 0)
            {
                request.Take = 1000;
            }

            var result = alertsService.GetAlerts(customerId, request);

            return Mapper.Map<PagedResult<PatientAlerts>, PagedResultDto<PatientAlertsDto>>(result,
                o => o.Items.Add("isBrief", request == null || request.IsBrief));
        }
    }
}