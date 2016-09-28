using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Domain.Dtos.VitalsService.Thresholds;

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// IVitalsDataProvider.
    /// </summary>
    public partial interface IVitalsDataProvider
    {
        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdSearchRequest">The search threshold request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<BaseThresholdDto>> GetThresholds(
            int customerId,
            Guid patientId,
            ThresholdsSearchDto thresholdSearchRequest,
            string bearerToken
        );

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateThreshold(
            int customerId, 
            CreateThresholdRequestDto request,
            string bearerToken
        );

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdateThreshold(int customerId, UpdateThresholdRequestDto request, string bearerToken);

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task DeleteThreshold(int customerId, Guid patientId, Guid thresholdId, string bearerToken);

        /// <summary>
        /// Search vitals.
        /// </summary>
        /// <param name="searchModel">search parameters</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PagedResult<MeasurementDto>> SearchVitals(SearchVitalsDto searchModel, string bearerToken);

        /// <summary>
        /// Gets the health sessions.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PagedResult<HealthSessionResponseDto>> GetHealthSessions(
            SearchHealthSessionsDto searchRequest,
            string bearerToken
        );

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<AlertSeverityResponseDto>> GetAlertSeverities(int customerId, string bearerToken);

        /// <summary>
        /// Maks measurement as invalidated
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        Task InvalidateMeasurement(int customerId, Guid patientId, Guid measurementId, string token);

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PagedResult<PatientAlertsDto>> GetAlerts(int customerId, SearchAlertsDto request, string token);

        /// <summary>
        /// Acknowledges alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="acknowledgeAlertsDto">The acknowledge alerts dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task AcknowledgeAlerts(int customerId, AcknowledgeAlertsRequestDto acknowledgeAlertsDto, string token);
    }
}