using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Web.Areas.Customer.Models.Settings.DefaultThresholds;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ISettingsControllerManager.DefaultThresholds.
    /// </summary>
    public partial interface ISettingsControllerManager
    {
        /// <summary>
        /// Gets customer's default thresholds.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CustomerDefaultThresholdsViewModel> GetDefaultThresholds(DefaultThresholdsSearchDto request);

        /// <summary>
        /// Creates customer's default threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateDefaultThreshold(CreateDefaultThresholdRequestDto request);

        /// <summary>
        /// Updates customer's default threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task UpdateDefaultThreshold(UpdateDefaultThresholdRequestDto request);

        /// <summary>
        /// Deletes customer's default threshold.
        /// </summary>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        Task DeleteDefaultThreshold(Guid defaultThresholdId);
    }
}