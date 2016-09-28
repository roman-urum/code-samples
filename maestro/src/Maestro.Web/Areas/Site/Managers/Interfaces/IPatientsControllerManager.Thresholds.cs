using System;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.VitalsService.Thresholds;
using Maestro.Web.Areas.Site.Models.Patients;

namespace Maestro.Web.Areas.Site.Managers.Interfaces
{
    /// <summary>
    /// IPatientsControllerManager.Thresholds
    /// </summary>
    public partial interface IPatientsControllerManager
    {
        /// <summary>
        /// Gets the thresholds.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<PatientThresholdsViewModel> GetThresholds(Guid patientId);

        /// <summary>
        /// Creates the threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateThreshold(CreateThresholdRequestDto request);

        /// <summary>
        /// Updates the threshold.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task UpdateThreshold(UpdateThresholdRequestDto request);

        /// <summary>
        /// Deletes the threshold.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="thresholdId">The threshold identifier.</param>
        /// <returns></returns>
        Task DeleteThreshold(Guid patientId, Guid thresholdId);
    }
}