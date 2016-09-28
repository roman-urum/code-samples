using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.Thresholds;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IThresholdsControllerHelper.
    /// </summary>
    public interface IThresholdsControllerHelper
    {
        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateThresholdStatus>> 
            CreateThreshold(int customerId, Guid patientId, ThresholdRequestDto request);

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<CreateUpdateThresholdStatus> 
            UpdateThreshold(int customerId, Guid patientId, Guid thresholdId, ThresholdRequestDto request);

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        Task<GetDeleteThresholdStatus> DeleteThreshold(int customerId, Guid patientId, Guid thresholdId);

        /// <summary>
        /// Gets the threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<PatientThresholdDto, GetDeleteThresholdStatus>>
            GetThreshold(int customerId, Guid patientId, Guid thresholdId);

        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<BaseThresholdDto>> GetThresholds(int customerId, Guid patientId, ThresholdsSearchDto request);
    }
}