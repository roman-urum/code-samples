using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// PatientConditionsService.
    /// </summary>
    public class PatientConditionsService : IPatientConditionsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Condition> conditionRepository;
        private readonly IRepository<PatientCondition> patientConditionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientConditionsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PatientConditionsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.conditionRepository = unitOfWork.CreateRepository<Condition>();
            this.patientConditionRepository = unitOfWork.CreateRepository<PatientCondition>();
        }

        #region Implementation of IPatientConditionsService

        /// <summary>
        /// Creates the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="patientConditionsIds">The patient conditions ids.</param>
        /// <returns></returns>
        public async Task<CreateUpdatePatientConditionsStatus> CreatePatientConditions(
            int customerId, 
            Guid patientId,
            IList<Guid> patientConditionsIds
        )
        {
            if (patientConditionsIds.Any())
            {
                var existingConditions = await conditionRepository
                    .FindAsync(
                        c => c.CustomerId == customerId &&
                        patientConditionsIds.Contains(c.Id)
                    );

                if (patientConditionsIds.Distinct().Count() != existingConditions.Count)
                {
                    return CreateUpdatePatientConditionsStatus.OneOfProvidedConditionsInvalid;
                }
            }

            var existingPatientConditions = await patientConditionRepository
                .FindAsync(
                    pc => pc.PatientId == patientId && 
                    pc.Condition.CustomerId == customerId,
                    null,
                    new List<Expression<Func<PatientCondition, object>>>
                    {
                        e => e.Condition
                    }
                );

            // Removing existing patient's conditions
            if (existingPatientConditions.Any())
            {
                patientConditionRepository.DeleteRange(existingPatientConditions);
            }

            // Setting new list of patient's conditions
            if (patientConditionsIds.Any())
            {
                patientConditionRepository.InsertRange(
                    patientConditionsIds
                        .Select(
                            c => new PatientCondition()
                            {
                                PatientId = patientId,
                                ConditionId = c
                            }
                        )
                        .ToList()
                );
            }

            if (existingPatientConditions.Any() || patientConditionsIds.Any())
            {
                await unitOfWork.SaveAsync();
            }

            return CreateUpdatePatientConditionsStatus.Success;
        }

        /// <summary>
        /// Gets the patient conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        public async Task<IList<Condition>> GetPatientConditions(int customerId, Guid patientId)
        {
            var existingPatientConditions = await patientConditionRepository
                .FindAsync(
                    pc => pc.PatientId == patientId &&
                    pc.Condition.CustomerId == customerId,
                    null,
                    new List<Expression<Func<PatientCondition, object>>>
                    {
                        e => e.Condition
                    }
                );

            return existingPatientConditions
                .Select(c => c.Condition)
                .ToList();
        }

        #endregion
    }
}