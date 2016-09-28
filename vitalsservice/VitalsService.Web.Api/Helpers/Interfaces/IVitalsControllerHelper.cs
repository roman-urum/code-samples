using System;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IVitalsControllerHelper.
    /// </summary>
    public interface IVitalsControllerHelper
    {
        /// <summary>
        /// Gets the vitals.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<MeasurementResponseDto>> GetVitals(
            int customerId, 
            Guid patientId,
            MeasurementsSearchDto request
        );

        /// <summary>
        /// Gets the vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <returns></returns>
        Task<MeasurementResponseDto> GetVital(
            int customerId,
            Guid patientId,
            Guid measurementId
        );

        /// <summary>
        /// Creates the vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<PostResponseDto<Guid>, CreateMeasurementStatus>> CreateVital(
            int customerId,
            Guid patientId,
            MeasurementRequestDto request
        );

        /// <summary>
        /// Updates the vital.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="measurementId">The measurement identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<UpdateMeasurementStatus> UpdateVital(
            int customerId, 
            Guid patientId,
            Guid measurementId, 
            UpdateMeasurementRequestDto request
        );
    }
}