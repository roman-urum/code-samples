using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IHealthSessionsService.
    /// </summary>
    public interface IHealthSessionsService
    {
        /// <summary>
        /// Creates new health session record.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<OperationResultDto<HealthSession, CreateHealthSessionStatus>> Create(HealthSession entity);

        /// <summary>
        /// Returns list of all health sessions for selected patient.
        /// </summary>
        /// <returns></returns>
        Task<PagedResult<HealthSession>> Find(
            int customerId,
            Guid patientId, 
            HealthSessionsSearchDto searchDto, 
            string clientId = null
        );

        /// <summary>
        /// Returns required health session by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<HealthSession> GetById(int customerId, Guid patientId, Guid id);

        /// <summary>
        /// Update the health session.
        /// </summary>
        /// <param name="healthSession">The health session instance.</param>
        /// <returns></returns>
        Task Update(HealthSession healthSession);
    }
}