using System;
using System.Net.Http;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models.AssessmentMedias;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Contains help methods for assessment media controller.
    /// </summary>
    public interface IAssessmentMediaControllerHelper
    {
        /// <summary>
        /// Creates new instance of AssessmentMedia using request model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<Guid> CreateAssessmentMedia(int customerId, Guid patientId, CreateAssessmentMediaRequestDto request);

        /// <summary>
        /// Saves file content for specified media.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="assessmentMediaId"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        Task<OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>> UpdateAssessmentMedia(
            int customerId, Guid patientId, Guid assessmentMediaId, HttpContent fileContent);

        /// <summary>
        /// Returns required assessment media by identifier.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="assessmentMediaId"></param>
        /// <returns></returns>
        Task<AssessmentMediaResponseDto> GetAssessmentMediaById(int customerId, Guid patientId,
            Guid assessmentMediaId);

        /// <summary>
        /// Returns all assessment media records for specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<PagedResult<AssessmentMediaResponseDto>> GetAllAssessmentMedia(int customerId, Guid patientId,
            AssessmentMediaSearchDto searchDto);
    }
}
