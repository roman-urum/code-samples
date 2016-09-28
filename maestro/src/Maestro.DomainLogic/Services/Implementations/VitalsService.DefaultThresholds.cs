using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Thresholds;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// VitalsService.DefaultThresholds
    /// </summary>
    public partial class VitalsService
    {
        /// <summary>
        /// Gets customer's default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        public Task<IList<DefaultThresholdDto>> GetDefaultThresholds(
            int customerId,
            DefaultThresholdsSearchDto request,
            string bearerToken
        )
        {
            return vitalsDataProvider.GetDefaultThresholds(customerId, request, bearerToken);
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
            return vitalsDataProvider.CreateDefaultThreshold(customerId, request, bearerToken);
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
            return vitalsDataProvider.UpdateDefaultThreshold(customerId, request, bearerToken);
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
            return vitalsDataProvider.DeleteDefaultThreshold(customerId, defaultThresholdId, bearerToken);
        }
    }
}