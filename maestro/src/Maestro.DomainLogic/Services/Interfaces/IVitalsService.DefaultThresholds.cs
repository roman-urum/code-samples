using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Thresholds;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IVitalsService.DefaultThresholds.
    /// </summary>
    public partial interface IVitalsService
    {
        /// <summary>
        /// Gets customer's default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<IList<DefaultThresholdDto>> GetDefaultThresholds(
            int customerId, 
            DefaultThresholdsSearchDto request, 
            string bearerToken
        );

        /// <summary>
        /// Creates customer's default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateDefaultThreshold(
            int customerId,
            CreateDefaultThresholdRequestDto request,
            string bearerToken
        );

        /// <summary>
        /// Updates customer's default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task UpdateDefaultThreshold(
            int customerId,
            UpdateDefaultThresholdRequestDto request,
            string bearerToken
        );

        /// <summary>
        /// Deletes customer's default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns></returns>
        Task DeleteDefaultThreshold(
            int customerId, 
            Guid defaultThresholdId, 
            string bearerToken
        );
    }
}