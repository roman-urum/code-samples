using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maestro.Common;
using Maestro.DataAccess.Api.ApiClient;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService;
using Maestro.Domain.Dtos.VitalsService.AlertSeverities;
using Maestro.Domain.Dtos.VitalsService.Enums;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using RestSharp;
using Maestro.Domain.Dtos.VitalsService.Alerts;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// VitalsDataProvider.
    /// </summary>
    public partial class VitalsDataProvider : IVitalsDataProvider
    {
        private readonly IRestApiClient apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="VitalsDataProvider" /> class.
        /// </summary>
        /// <param name="apiClientFactory">The API client factory.</param>
        public VitalsDataProvider(IRestApiClientFactory apiClientFactory)
        {
            this.apiClient = apiClientFactory.Create(Settings.VitalsServiceUrl);
        }

        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdSearchRequest">The search threshold request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<BaseThresholdDto>> GetThresholds(
            int customerId,
            Guid patientId,
            ThresholdsSearchDto thresholdSearchRequest,
            string bearerToken
        )
        {
            var parametersQueryStrings = new List<string>();

            if (thresholdSearchRequest != null && thresholdSearchRequest.Mode.HasValue)
            {
                parametersQueryStrings.Add(string.Format("mode={0}", thresholdSearchRequest.Mode.Value));
            }

            if (thresholdSearchRequest != null && thresholdSearchRequest.ConditionIds != null && thresholdSearchRequest.ConditionIds.Any())
            {
                parametersQueryStrings.AddRange(thresholdSearchRequest.ConditionIds.Select(c => string.Format("condition={0}", c)));
            }

            if (thresholdSearchRequest != null && !string.IsNullOrEmpty(thresholdSearchRequest.Q))
            {
                parametersQueryStrings.Add(string.Format("q={0}", thresholdSearchRequest.Q));
            }

            if (thresholdSearchRequest != null && thresholdSearchRequest.Skip > 0)
            {
                parametersQueryStrings.Add(string.Format("skip={0}", thresholdSearchRequest.Skip));
            }

            if (thresholdSearchRequest != null && thresholdSearchRequest.Take > 0)
            {
                parametersQueryStrings.Add(string.Format("take={0}", thresholdSearchRequest.Take));
            }

            var queryString = parametersQueryStrings.Aggregate((s1, s2) => string.Format("{0}&{1}", s1, s2));

            string endpointUrl = string.Format("api/{0}/thresholds/{1}?{2}", customerId, patientId, queryString);

            var response = await apiClient.SendRequestAsync<PagedResult<BaseThresholdDto>>(endpointUrl, null, Method.GET, null, bearerToken);

            return response.Results;
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
            return await apiClient.SendRequestAsync<PostResponseDto<Guid>>(
                string.Format("/api/{0}/thresholds/{1}", customerId, request.PatientId),
                request,
                Method.POST,
                null,
                bearerToken
            );
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
            return apiClient.SendRequestAsync(
                string.Format("/api/{0}/thresholds/{1}/{2}", customerId, request.PatientId, request.Id),
                request,
                Method.PUT,
                null,
                bearerToken
            );
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
            string url = string.Format("/api/{0}/thresholds/{1}/{2}", customerId, patientId, thresholdId);

            return apiClient.SendRequestAsync(url, null, Method.DELETE, null, bearerToken);
        }

        /// <summary>
        /// Search vitals.
        /// </summary>
        /// <param name="searchModel">search parameters</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PagedResult<MeasurementDto>> SearchVitals(SearchVitalsDto searchModel, string bearerToken)
        {
            string url = string.Format("/api/{0}/vitals/{1}?IsInvalidated={2}", 
                searchModel.CustomerId, 
                searchModel.PatientId,
                searchModel.IsInvalidated);

            if (searchModel.ObservedFrom.HasValue)
            {
                url += string.Format("&ObservedFrom={0}", searchModel.ObservedFrom);
            }

            if (searchModel.ObservedTo.HasValue)
            {
                url += string.Format("&ObservedTo={0}", searchModel.ObservedTo);
            }

            if (!string.IsNullOrEmpty(searchModel.Q))
            {
                url += string.Format("&Q={0}", searchModel.Q);
            }

            return apiClient.SendRequestAsync<PagedResult<MeasurementDto>>(url, null, Method.GET, null, bearerToken);
        }

        /// <summary>
        /// Gets the health sessions.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<PagedResult<HealthSessionResponseDto>> GetHealthSessions(
            SearchHealthSessionsDto searchRequest,
            string bearerToken
        )
        {
            if (searchRequest == null)
            {
                throw new ArgumentNullException("searchRequest");
            }

            var result = await apiClient.SendRequestAsync<PagedResult<HealthSessionResponseDto>>(
                string.Format("/api/{0}/sessions/{1}", searchRequest.CustomerId, searchRequest.PatientId),
                searchRequest,
                Method.GET,
                null, 
                bearerToken
            );

            return result;
        }

        /// <summary>
        /// Gets the alert severities.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<AlertSeverityResponseDto>> GetAlertSeverities(int customerId, string bearerToken)
        {
            var pagedResult = await apiClient
                .SendRequestAsync<PagedResult<AlertSeverityResponseDto>>(
                    string.Format("/api/{0}/severities", customerId),
                    null,
                    Method.GET,
                    null,
                    bearerToken
                );

            return pagedResult.Results;
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
            string url = string.Format("/api/{0}/vitals/{1}/{2}", customerId, patientId, measurementId);

            await apiClient.SendRequestAsync<PagedResult<AlertSeverityResponseDto>>(
                url,
                new InvalidateAlertDto() { IsInvalidated = true },
                Method.PUT,
                null,
                token
            );
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
            string endpointUrl = string.Format("api/{0}/alerts/acknowledge", customerId);

            await apiClient.SendRequestAsync(
                endpointUrl,
                acknowledgeAlertsDto,
                Method.POST,
                null,
                token
            );
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
            string endpointUrl = string.Format("api/{0}/alerts/getalerts", customerId);

            var response = await apiClient.SendRequestAsync<PagedResult<PatientAlertsDto>>(
                endpointUrl,
                request,
                Method.POST,
                null,
                token
            );

            return response;
        }
    }
}