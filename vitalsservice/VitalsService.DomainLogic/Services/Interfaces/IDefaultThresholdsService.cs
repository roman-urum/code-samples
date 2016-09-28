using System;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IDefaultThresholdsService.
    /// </summary>
    public interface IDefaultThresholdsService
    {
        /// <summary>
        /// Creates the default threshold.
        /// </summary>
        /// <param name="defaultThreshold">The default threshold.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateDefaultThresholdStatus>> CreateDefaultThreshold(DefaultThreshold defaultThreshold);

        /// <summary>
        /// Updates the default threshold.
        /// </summary>
        /// <param name="defaultThreshold">The default threshold.</param>
        /// <returns></returns>
        Task<CreateUpdateDefaultThresholdStatus> UpdateDefaultThreshold(DefaultThreshold defaultThreshold);

        /// <summary>
        /// Deletes the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        Task<GetDeleteDefaultThresholdStatus> DeleteDefaultThreshold(int customerId, Guid defaultThresholdId);

        /// <summary>
        /// Gets the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        Task<DefaultThreshold> GetDefaultThreshold(int customerId, Guid defaultThresholdId);

        /// <summary>
        /// Gets the default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<DefaultThreshold>> GetDefaultThresholds(int customerId, DefaultThresholdsSearchDto request);
    }
}