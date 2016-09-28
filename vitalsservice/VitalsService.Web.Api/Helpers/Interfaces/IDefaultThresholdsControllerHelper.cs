using System;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Thresholds;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IDefaultThresholdsControllerHelper.
    /// </summary>
    public interface IDefaultThresholdsControllerHelper
    {
        /// <summary>
        /// Creates the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateDefaultThresholdStatus>> 
            CreateDefaultThreshold(int customerId, DefaultThresholdRequestDto request);

        /// <summary>
        /// Updates the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateUpdateDefaultThresholdStatus>
            UpdateDefaultThreshold(int customerId, Guid defaultThresholdId, DefaultThresholdRequestDto request);

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
        Task<OperationResultDto<DefaultThresholdDto, GetDeleteDefaultThresholdStatus>>
            GetDefaultThreshold(int customerId, Guid defaultThresholdId);

        /// <summary>
        /// Gets the default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<DefaultThresholdDto>> GetDefaultThresholds(int customerId, DefaultThresholdsSearchDto request);
    }
}