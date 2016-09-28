using System;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IAlertsService.
    /// </summary>
    public interface IAlertSeveritiesService
    {
        /// <summary>
        /// Creates the alert severity.
        /// </summary>
        /// <param name="alertSeverity">The alert severity.</param>
        /// <returns></returns>
        Task<AlertSeverity> CreateAlertSeverity(AlertSeverity alertSeverity);

        /// <summary>
        /// Updates the alert severity.
        /// </summary>
        /// <param name="alertSeverity">The alert severity.</param>
        /// <returns></returns>
        Task<CreateUpdateDeleteAlertSeverityStatus> UpdateAlertSeverity(AlertSeverity alertSeverity);

        /// <summary>
        /// Gets the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<AlertSeverity, GetAlertSeverityStatus>> GetAlertSeverity(int customerId, Guid id);

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResult<AlertSeverity>> GetAlertSeverities(int customerId, AlertSeveritiesSearchDto searchRequest);

        /// <summary>
        /// Deletes the alert severity.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="alertSeverityId">The identifier.</param>
        /// <returns></returns>
        Task<CreateUpdateDeleteAlertSeverityStatus> DeleteAlertSeverity(int customerId, Guid alertSeverityId);
    }
}