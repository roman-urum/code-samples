using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// VitalsDataProvider.DefaultThresholds
    /// </summary>
    public partial class VitalsDataProvider
    {
        /// <summary>
        /// Gets customer's default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public async Task<IList<DefaultThresholdDto>> GetDefaultThresholds(
            int customerId, 
            DefaultThresholdsSearchDto request, 
            string bearerToken
        )
        {
            string endpointUrl = string.Format("api/{0}/thresholds/defaults", customerId);
            
            var result = await apiClient.SendRequestAsync<PagedResult<DefaultThresholdDto>>(
                endpointUrl,
                request,
                Method.GET, 
                null, 
                bearerToken
            );

            return result.Results;
        }

        /// <summary>
        /// Creates customer's default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<PostResponseDto<Guid>> CreateDefaultThreshold(
            int customerId,
            CreateDefaultThresholdRequestDto request,
            string bearerToken
        )
        {
            return apiClient.SendRequestAsync<PostResponseDto<Guid>>(
                string.Format("/api/{0}/thresholds/defaults", customerId),
                request,
                Method.POST,
                null,
                bearerToken
            );
        }

        /// <summary>
        /// Updates customer's default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task UpdateDefaultThreshold(
            int customerId,
            UpdateDefaultThresholdRequestDto request,
            string bearerToken
        )
        {
            return apiClient.SendRequestAsync(
                string.Format("/api/{0}/thresholds/defaults/{1}", customerId, request.Id),
                request,
                Method.PUT,
                null,
                bearerToken
            );
        }

        /// <summary>
        /// Deletes customer's default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task DeleteDefaultThreshold(
            int customerId,
            Guid defaultThresholdId, 
            string bearerToken
        )
        {
            string url = string.Format("/api/{0}/thresholds/defaults/{1}", customerId, defaultThresholdId);

            return apiClient.SendRequestAsync(url, null, Method.DELETE, null, bearerToken);
        }
    }
}