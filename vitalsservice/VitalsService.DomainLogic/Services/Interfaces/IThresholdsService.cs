using System;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IThresholdsService
    /// </summary>
    public interface IThresholdsService
    {
        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateUpdateThresholdStatus>> CreateThreshold(PatientThreshold threshold);

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="threshold">The threshold.</param>
        /// <returns></returns>
        Task<CreateUpdateThresholdStatus> UpdateThreshold(PatientThreshold threshold);

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
        Task<PatientThreshold> GetThreshold(int customerId, Guid patientId, Guid thresholdId);

        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<PatientThreshold>> GetThresholds(int customerId, Guid patientId, BaseSearchDto request);
    }
}