using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.Alerts;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.DomainLogic.Services.Interfaces;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// VitalsService.
    /// </summary>
    public partial class VitalsService : IVitalsService
    {
        private readonly IVitalsDataProvider vitalsDataProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalsService" /> class.
        /// </summary>
        /// <param name="vitalsDataProvider">The vitals data provider.</param>
        public VitalsService(IVitalsDataProvider vitalsDataProvider)
        {
            this.vitalsDataProvider = vitalsDataProvider;
        }

        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdSearchType">Type of the threshold search.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<BaseThresholdDto>> GetThresholds(
            int customerId,
            Guid patientId,
            ThresholdSearchType thresholdSearchType,
            string bearerToken
        )
        {
            return await vitalsDataProvider.GetThresholds(
                    customerId,
                    patientId,
                    new ThresholdsSearchDto() { Mode = thresholdSearchType },
                    bearerToken);
        }

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateThreshold(
            int customerId,
            CreateThresholdRequestDto request,
            string bearerToken
        )
        {
            return await vitalsDataProvider.CreateThreshold(customerId, request, bearerToken);
        }

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateThreshold(int customerId, UpdateThresholdRequestDto request, string bearerToken)
        {
            return vitalsDataProvider.UpdateThreshold(customerId, request, bearerToken);
        }

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task DeleteThreshold(int customerId, Guid patientId, Guid thresholdId, string bearerToken)
        {
            return vitalsDataProvider.DeleteThreshold(customerId, patientId, thresholdId, bearerToken);
        }

        /// <summary>
        /// Search vitals.
        /// </summary>
        /// <param name="searchModel">search parameters</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PagedResult<MeasurementDto>> GetVitals(SearchVitalsDto searchModel, string bearerToken)
        {
            return vitalsDataProvider.SearchVitals(searchModel, bearerToken);
        }

        /// <summary>
        /// Gets the health sessions.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PagedResult<HealthSessionResponseDto>> GetHealthSessions(
            SearchHealthSessionsDto searchRequest, 
            string bearerToken
        )
        {
            return vitalsDataProvider.GetHealthSessions(searchRequest, bearerToken);
        }

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<AlertSeverityResponseDto>> GetAlertSeverities(int customerId, string bearerToken)
        {
            return await vitalsDataProvider.GetAlertSeverities(customerId, bearerToken);
        }

        /// <summary>
        /// Maks measurement as invalidated
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="token">The bearer token.</param>
        /// <returns></returns>
        public async Task InvalidateMeasurement(int customerId, Guid patientId, Guid measurementId, string token)
        {
            await vitalsDataProvider.InvalidateMeasurement(customerId, patientId, measurementId, token);
        }

        /// <summary>
        /// Acknowledges alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="acknowledgeAlertsDto">The acknowledge alerts dto.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task AcknowledgeAlerts(int customerId, AcknowledgeAlertsRequestDto acknowledgeAlertsDto, string token)
        {
            await vitalsDataProvider.AcknowledgeAlerts(customerId, acknowledgeAlertsDto, token);
        }

        /// <summary>
        /// Gets the alerts.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<PagedResult<PatientAlertsDto>> GetAlerts(int customerId, SearchAlertsDto request, string token)
        {
            return await vitalsDataProvider.GetAlerts(customerId, request, token);
        }
    }
}