using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.DataAccess.EF;
using VitalsService.DataAccess.EF.Repositories;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Helpers;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// DefaultThresholdsService.
    /// </summary>
    public class DefaultThresholdsService : IDefaultThresholdsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<DefaultThreshold> defaultThresholdRepository;
        private readonly IRepository<AlertSeverity> alertSeverityRepository;
        private readonly IRepository<VitalAlert> vitalAlertsRepository;
        private readonly IRepository<Condition> conditionsRepository; 

        /// <summary>
        /// Initializes a new instance of the <see cref="ThresholdsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public DefaultThresholdsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.defaultThresholdRepository = unitOfWork.CreateRepository<DefaultThreshold>();
            this.alertSeverityRepository = unitOfWork.CreateRepository<AlertSeverity>();
            this.vitalAlertsRepository = unitOfWork.CreateRepository<VitalAlert>();
            conditionsRepository = unitOfWork.CreateRepository<Condition>();
        }

        /// <summary>
        /// Creates the default threshold.
        /// </summary>
        /// <param name="defaultThreshold">The default threshold.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateUpdateDefaultThresholdStatus>> CreateDefaultThreshold(DefaultThreshold defaultThreshold)
        {
            var validationResult = await ValidateDefaultThreshold(defaultThreshold);

            if (!validationResult.HasFlag(CreateUpdateDefaultThresholdStatus.Success))
            {
                return new OperationResultDto<Guid, CreateUpdateDefaultThresholdStatus>() { Status = validationResult };
            }

            defaultThresholdRepository.Insert(defaultThreshold);
            await unitOfWork.SaveAsync();

            return await Task.FromResult(
                new OperationResultDto<Guid, CreateUpdateDefaultThresholdStatus>()
                {
                    Status = CreateUpdateDefaultThresholdStatus.Success,
                    Content = defaultThreshold.Id
                }
            );
        }

        private async Task<CreateUpdateDefaultThresholdStatus> ValidateDefaultThreshold(DefaultThreshold defaultThreshold)
        {
            var existingAlertSeverities = await alertSeverityRepository.FindAsync(a => a.CustomerId == defaultThreshold.CustomerId);

            if (defaultThreshold.AlertSeverityId.HasValue)
            {

                if (existingAlertSeverities.All(a => a.Id != defaultThreshold.AlertSeverityId.Value))
                {
                    return CreateUpdateDefaultThresholdStatus.AlertSeverityDoesNotExist;
                }
            }
            else
            {
                if (existingAlertSeverities.Any())
                {
                    return CreateUpdateDefaultThresholdStatus.ExistingAlertSeverityShouldBeUsed;
                }
            }

            if (defaultThreshold.DefaultType.ToLower() == ThresholdDefaultType.Customer.ToString().ToLower())
            {
                var existingVitalDefaultThresholds = await defaultThresholdRepository.FindAsync(t =>
                    t.CustomerId == defaultThreshold.CustomerId &&
                    t.DefaultType.ToLower() == ThresholdDefaultType.Customer.ToString().ToLower() &&
                    t.Name.ToLower() == defaultThreshold.Name.ToLower() &&
                    t.AlertSeverityId == defaultThreshold.AlertSeverityId
                );

                if (existingVitalDefaultThresholds.Any())
                {
                    return CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdAlreadyExists;
                }                
            }

            if (defaultThreshold.DefaultType.ToLower() == ThresholdDefaultType.Condition.ToString().ToLower())
            {
                if (!defaultThreshold.ConditionId.HasValue)
                {
                    return CreateUpdateDefaultThresholdStatus.ConditionShouldBeSpecified;
                }

                var existingConditionIds = (await conditionsRepository.FindAsync(c => c.CustomerId == defaultThreshold.CustomerId)).Select(c => c.Id);

                if (existingConditionIds.All(c => c != defaultThreshold.ConditionId.Value))
                {
                    return CreateUpdateDefaultThresholdStatus.ConditionDoesNotExist;
                }

                var existingVitalConditionThresholds = await defaultThresholdRepository.FindAsync(t =>
                    t.CustomerId == defaultThreshold.CustomerId &&
                    t.DefaultType.ToLower() == ThresholdDefaultType.Condition.ToString().ToLower() &&
                    t.Name.ToLower() == defaultThreshold.Name.ToLower() &&
                    t.AlertSeverityId == defaultThreshold.AlertSeverityId && 
                    t.ConditionId == defaultThreshold.ConditionId
                );

                if (existingVitalConditionThresholds.Any())
                {
                    return CreateUpdateDefaultThresholdStatus.VitalConditionThresholdAlreadyExists;
                }                
            }

            return CreateUpdateDefaultThresholdStatus.Success;
        }

        /// <summary>
        /// Updates the default threshold.
        /// </summary>
        /// <param name="defaultThreshold">The default threshold.</param>
        /// <returns></returns>
        public async Task<CreateUpdateDefaultThresholdStatus> UpdateDefaultThreshold(DefaultThreshold defaultThreshold)
        {
            var existingCustomerDefaultThresholds = await defaultThresholdRepository
                .FindAsync(t =>
                    t.CustomerId == defaultThreshold.CustomerId
                );

            var requestedCustomerDefaultThreshold = existingCustomerDefaultThresholds.SingleOrDefault(t => t.Id == defaultThreshold.Id);

            if (requestedCustomerDefaultThreshold == null)
            {
                return await Task.FromResult(CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdDoesNotExist);
            }

            var existingAlertSeverities = await alertSeverityRepository
                .FindAsync(a => a.CustomerId == defaultThreshold.CustomerId);

            if (defaultThreshold.AlertSeverityId.HasValue)
            {
                var existingAlertSeverity =
                    existingAlertSeverities.SingleOrDefault(a => a.Id == defaultThreshold.AlertSeverityId.Value);

                if (existingAlertSeverity == null)
                {
                    return await Task.FromResult(CreateUpdateDefaultThresholdStatus.AlertSeverityDoesNotExist);
                }
            }
            else
            {
                if (existingAlertSeverities.Any())
                {
                    return await Task.FromResult(CreateUpdateDefaultThresholdStatus.ExistingAlertSeverityShouldBeUsed);
                }
            }

            if (defaultThreshold.DefaultType.ToLower() == ThresholdDefaultType.Customer.ToString().ToLower())
            {
                if (
                    requestedCustomerDefaultThreshold.Name.ToLower() != defaultThreshold.Name.ToLower() ||
                    requestedCustomerDefaultThreshold.AlertSeverityId != defaultThreshold.AlertSeverityId
                )
                {
                    var conflictingCustomerDefaultThresholds = existingCustomerDefaultThresholds
                        .Where(t =>
                            t.DefaultType.ToString().ToLower() == ThresholdDefaultType.Customer.ToString().ToLower() &&
                            t.Id != defaultThreshold.Id &&
                            t.Name.ToLower() == defaultThreshold.Name.ToLower() &&
                            t.AlertSeverityId == defaultThreshold.AlertSeverityId
                        )
                        .ToList();

                    if (conflictingCustomerDefaultThresholds.Any())
                    {
                        return await Task.FromResult(CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdAlreadyExists);
                    }
                }                
            }
            else if (defaultThreshold.DefaultType.ToLower() == ThresholdDefaultType.Condition.ToString().ToLower())
            {
                if (
                    requestedCustomerDefaultThreshold.Name.ToLower() != defaultThreshold.Name.ToLower() ||
                    requestedCustomerDefaultThreshold.AlertSeverityId != defaultThreshold.AlertSeverityId ||
                    requestedCustomerDefaultThreshold.ConditionId != defaultThreshold.ConditionId
                )
                {
                    var conflictingConditionDefaultThresholds = existingCustomerDefaultThresholds
                        .Where(t =>
                            t.DefaultType.ToString().ToLower() == ThresholdDefaultType.Condition.ToString().ToLower() &&
                            t.Id != defaultThreshold.Id &&
                            t.Name.ToLower() == defaultThreshold.Name.ToLower() &&
                            t.AlertSeverityId == defaultThreshold.AlertSeverityId &&
                            t.ConditionId == defaultThreshold.ConditionId
                        )
                        .ToList();

                    if (conflictingConditionDefaultThresholds.Any())
                    {
                        return await Task.FromResult(CreateUpdateDefaultThresholdStatus.VitalDefaultThresholdAlreadyExists);
                    }
                }
            }


            Mapper.Map(defaultThreshold, requestedCustomerDefaultThreshold, typeof(DefaultThreshold), typeof(DefaultThreshold));

            defaultThresholdRepository.Update(requestedCustomerDefaultThreshold);
            await unitOfWork.SaveAsync();

            return CreateUpdateDefaultThresholdStatus.Success;
        }

        /// <summary>
        /// Deletes the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        public async Task<GetDeleteDefaultThresholdStatus> DeleteDefaultThreshold(int customerId, Guid defaultThresholdId)
        {
            var existingDefaultThreshold =
                await GetDefaultThreshold(customerId, defaultThresholdId);

            if (existingDefaultThreshold != null)
            {
                var vitalAlerts = existingDefaultThreshold.VitalAlerts.ToList();

                // Explicitly removing all Vital alerts and alert details dependent on this default threshold
                foreach (var vitalAlert in vitalAlerts)
                {
                    vitalAlertsRepository.Delete(vitalAlert);
                }

                defaultThresholdRepository.Delete(existingDefaultThreshold);

                await unitOfWork.SaveAsync();

                return await Task.FromResult(GetDeleteDefaultThresholdStatus.Success);
            }

            return await Task.FromResult(GetDeleteDefaultThresholdStatus.NotFound);
        }

        /// <summary>
        /// Gets the default threshold.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="defaultThresholdId">The default threshold identifier.</param>
        /// <returns></returns>
        public async Task<DefaultThreshold> GetDefaultThreshold(
            int customerId,
            Guid defaultThresholdId
        )
        {
            return await defaultThresholdRepository
                .FirstOrDefaultAsync(
                    t => t.CustomerId == customerId &&
                    t.Id == defaultThresholdId
                );
        }

        /// <summary>
        /// Gets the default thresholds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResult<DefaultThreshold>> GetDefaultThresholds(int customerId, DefaultThresholdsSearchDto request)
        {
            Expression<Func<DefaultThreshold, bool>> expression = t => t.CustomerId == customerId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(t => t.Name.Contains(term));
                    }
                }

                if (request.DefaultType.HasValue && request.DefaultType == ThresholdDefaultType.Customer)
                {
                    expression = expression.And(t => t.DefaultType.ToLower() == request.DefaultType.Value.ToString().ToLower());
                }

                if (request.DefaultType.HasValue && request.DefaultType == ThresholdDefaultType.Condition)
                {
                    var conditionIds = request.ConditionIds ?? new List<Guid>();

                    expression = expression.And(t => t.DefaultType.ToLower() == request.DefaultType.Value.ToString().ToLower()
                                                     && (!conditionIds.Any() || (t.ConditionId.HasValue && conditionIds.Contains(t.ConditionId.Value))));
                }

                if (request.AlertSeverityId.HasValue)
                {
                    expression = expression.And(
                        t => t.AlertSeverityId == request.AlertSeverityId.Value
                    );
                }
            }

            //this is to avoid getting corrupted condition thresholds with null conditionid.
            expression = expression.And(t => t.DefaultType.ToLower() != ThresholdDefaultType.Condition.ToString().ToLower() || t.ConditionId.HasValue);

            return await defaultThresholdRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    new List<Expression<Func<DefaultThreshold, object>>>
                    {
                        s => s.AlertSeverity
                    },
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }
    }
}