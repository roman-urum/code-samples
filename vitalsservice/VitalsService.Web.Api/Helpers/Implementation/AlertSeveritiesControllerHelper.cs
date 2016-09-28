using System;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Alerts;
using VitalsService.Web.Api.Models.AlertSeverities;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// AlertSeveritiesControllerHelper.
    /// </summary>
    /// <seealso cref="VitalsService.Web.Api.Helpers.Interfaces.IAlertSeveritiesControllerHelper" />
    public class AlertSeveritiesControllerHelper : IAlertSeveritiesControllerHelper
    {
        private readonly IAlertSeveritiesService alertSeveritiesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertSeveritiesControllerHelper" /> class.
        /// </summary>
        /// <param name="alertSeveritiesService">The alert severities service.</param>
        public AlertSeveritiesControllerHelper(IAlertSeveritiesService alertSeveritiesService)
        {
            this.alertSeveritiesService = alertSeveritiesService;
        }

        /// <summary>
        /// Creates the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<Guid> CreateAlertSeverity(int customerId, AlertSeverityRequestDto request)
        {
            var alertSeverityEntity = Mapper.Map<AlertSeverity>(request);
            alertSeverityEntity.CustomerId = customerId;

            var result = await alertSeveritiesService.CreateAlertSeverity(alertSeverityEntity);

            return result.Id;
        }

        /// <summary>
        /// Updates the alert severity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<CreateUpdateDeleteAlertSeverityStatus> UpdateAlertSeverity(Guid id, int customerId, AlertSeverityRequestDto request)
        {
            var alertSeverityEntity = Mapper.Map<AlertSeverity>(request);
            alertSeverityEntity.Id = id;
            alertSeverityEntity.CustomerId = customerId;

            return await this.alertSeveritiesService.UpdateAlertSeverity(alertSeverityEntity);
        }

        /// <summary>
        /// Deletes the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<CreateUpdateDeleteAlertSeverityStatus> DeleteAlertSeverity(int customerId, Guid id)
        {
            return await alertSeveritiesService.DeleteAlertSeverity(customerId, id);
        }

        /// <summary>
        /// Gets the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<AlertSeverityResponseDto, GetAlertSeverityStatus>> GetAlertSeverity(int customerId, Guid id)
        {
            var operationResult = await alertSeveritiesService.GetAlertSeverity(customerId, id);

            return new OperationResultDto<AlertSeverityResponseDto, GetAlertSeverityStatus>()
            {
                Content = Mapper.Map<AlertSeverityResponseDto>(operationResult.Content),
                Status = operationResult.Status
            };
        }

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<AlertSeverityResponseDto>> GetAlertSeverities(int customerId, AlertSeveritiesSearchDto searchRequest)
        {
            var alertSeverities = await alertSeveritiesService.GetAlertSeverities(customerId, searchRequest);

            return Mapper.Map<PagedResult<AlertSeverity>, PagedResultDto<AlertSeverityResponseDto>>(alertSeverities);
        }
    }
}