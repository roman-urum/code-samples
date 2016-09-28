using System;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business logic for assessment media entities.
    /// </summary>
    public interface IAssessmentMediaService
    {
        /// <summary>
        /// Creates new instance of assessment media in db.
        /// </summary>
        /// <param name="assessmentMedia"></param>
        /// <returns></returns>
        Task<AssessmentMedia> Create(AssessmentMedia assessmentMedia);

        /// <summary>
        /// Updates storage key for assessment media if storage key is not set yet.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>> Update(int customerId,
            Guid patientId, Guid id, byte[] file);

        /// <summary>
        /// Returns required assessment media by identifier.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AssessmentMedia> GetById(int customerId, Guid patientId, Guid id);

        /// <summary>
        /// Returns all records matches to specified criteria.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        Task<PagedResult<AssessmentMedia>> GetAll(int customerId, Guid patientId,
            AssessmentMediaSearchDto searchDto);
    }
}
