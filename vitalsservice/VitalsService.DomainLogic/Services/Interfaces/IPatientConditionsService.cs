using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IPatientConditionsService
    /// </summary>
    public interface IPatientConditionsService
    {
        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="patientConditionsIds">The patient conditions ids.</param>
        /// <returns></returns>
        Task<CreateUpdatePatientConditionsStatus> CreatePatientConditions(
            int customerId, 
            Guid patientId,
            IList<Guid> patientConditionsIds
        );

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        Task<IList<Condition>> GetPatientConditions(int customerId, Guid patientId);
    }
}