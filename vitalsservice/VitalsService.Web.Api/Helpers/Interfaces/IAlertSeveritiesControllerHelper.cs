using System;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Alerts;
using VitalsService.Web.Api.Models.AlertSeverities;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IAlertSeveritiesControllerHelper.
    /// </summary>
    public interface IAlertSeveritiesControllerHelper
    {
        /// <summary>
        /// Creates the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<Guid> CreateAlertSeverity(int customerId, AlertSeverityRequestDto request);

        /// <summary>
        /// Updates the alert severity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateUpdateDeleteAlertSeverityStatus> UpdateAlertSeverity(Guid id, int customerId, AlertSeverityRequestDto request);

        /// <summary>
        /// Deletes the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<CreateUpdateDeleteAlertSeverityStatus> DeleteAlertSeverity(int customerId, Guid id);

        /// <summary>
        /// Gets the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<AlertSeverityResponseDto, GetAlertSeverityStatus>> GetAlertSeverity(int customerId, Guid id);

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResultDto<AlertSeverityResponseDto>> GetAlertSeverities(int customerId, AlertSeveritiesSearchDto searchRequest);
    }
}