using System;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.HealthSessions;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Contains health sessions models mapping and presentation
    /// layer logic.
    /// </summary>
    public interface IHealthSessionsControllerHelper
    {
        /// <summary>
        /// Creates entity from request model and save data.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<OperationResultDto<PostResponseDto<Guid>, CreateHealthSessionStatus>> Create(
            HealthSessionRequestDto model,
            int customerId,
            Guid patientId
        );

        /// <summary>
        /// Returns all health sessions for patient.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="searchDto">The search dto.</param>
        /// <returns></returns>
        Task<PagedResultDto<HealthSessionResponseDto>> Find(
            int customerId, 
            Guid patientId,
            HealthSessionsSearchDto searchDto
        );

        /// <summary>
        /// Returns health session with specified id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="healthSessionId"></param>
        /// <returns></returns>
        Task<OperationResultDto<HealthSessionResponseDto, GetHealthSessionStatus>> GetById(int customerId, Guid patientId, Guid healthSessionId);

        /// <summary>
        /// Uppdate the health session.
        /// </summary>
        /// <param name="model">The update model.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="healthSessionId">The health session identifier to be updated.</param>
        /// <returns></returns>
        Task<UpdateHealthSessionStatus> Update(UpdateHealthSessionRequestDto model, int customerId, Guid patientId, Guid healthSessionId);
    }
}