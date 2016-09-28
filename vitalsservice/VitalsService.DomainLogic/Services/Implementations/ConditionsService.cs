using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
    /// ConditionService
    /// </summary>
    public class ConditionsService: IConditionsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Condition> conditionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionsService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unitOfWork.</param>
        public ConditionsService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            conditionRepository = unitOfWork.CreateRepository<Condition>();
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier</param>
        /// <returns>Condition entity or null</returns>
        public async Task<Condition> GetCondition(int customerId, Guid conditionId)
        {
            var includeProperties = new List<Expression<Func<Condition, object>>>()
            {
                e => e.DefaultThresholds
            };

            return (await conditionRepository.FindAsync(c => c.CustomerId == customerId && c.Id == conditionId, null, includeProperties)).FirstOrDefault();
        }

        /// <summary>
        /// Get the list of conditions.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The search request.</param>
        /// <returns>The list of conditions.</returns>
        public async Task<PagedResult<Condition>> GetConditions(int customerId, ConditionSearchDto request)
        {
            Expression<Func<Condition, bool>> expression = c => c.CustomerId == customerId;

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(c => c.Name.Contains(term));
                    }
                }

                if (request.Tags != null && request.Tags.Any())
                {
                    Expression<Func<Condition, bool>> tagsExpression = null;

                    foreach (var tag in request.Tags)
                    {
                        if (tagsExpression == null)
                        {
                            tagsExpression = c => c.Tags.Any(t => t.Name.ToLower() == tag.ToLower());
                        }
                        else
                        {
                            tagsExpression = tagsExpression.Or(c => c.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
                        }                        
                    }
                    expression = expression.And(tagsExpression);
                }
            }

            return await conditionRepository
                .FindPagedAsync(
                    expression,
                    o => o.OrderBy(e => e.Id),
                    null,
                    request != null ? request.Skip : (int?)null,
                    request != null ? request.Take : (int?)null
                );
        }

        /// <summary>
        /// Creates condition.
        /// </summary>
        /// <param name="request">The condition entity.</param>
        /// <returns>The creation result.</returns>
        public async Task<OperationResultDto<Guid, ConditionStatus>> CreateCondition(Condition request)
        {
            if ((await conditionRepository.FindAsync(c => c.Name == request.Name && c.CustomerId == request.CustomerId)).Any())
            {
                return new OperationResultDto<Guid, ConditionStatus>()
                {
                    Status = ConditionStatus.ConditionAlreadyExists
                };
            }

            conditionRepository.Insert(request);
            await unitOfWork.SaveAsync();

            return new OperationResultDto<Guid, ConditionStatus>()
            {
                Status = ConditionStatus.Success,
                Content = request.Id
            };
        }

        /// <summary>
        /// Updates the condition.
        /// </summary>
        /// <param name="request">The condition entity.</param>
        /// <returns>The update result.</returns>
        public async Task<ConditionStatus> UpdateCondition(Condition request)
        {
            var existedCondition = await GetCondition(request.CustomerId, request.Id);

            if (existedCondition == null)
            {
                return ConditionStatus.NotFound;
            }

            if ((await conditionRepository.FindAsync(c => c.Name == request.Name && c.CustomerId == request.CustomerId && c.Id != request.Id)).Any())
            {
                return  ConditionStatus.ConditionAlreadyExists;
            }

            Mapper.Map<Condition, Condition>(request, existedCondition);

            conditionRepository.Update(existedCondition);
            await unitOfWork.SaveAsync();

            return ConditionStatus.Success;
        }

        /// <summary>
        /// Deletes the condition.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="conditionId">The condition identifier.</param>
        /// <returns>The delete result.</returns>
        public async Task<ConditionStatus> DeleteCondition(int customerId, Guid conditionId)
        {
            var existedCondition = await GetCondition(customerId, conditionId);

            if (existedCondition == null)
            {
                return ConditionStatus.NotFound;
            }

            conditionRepository.Delete(existedCondition);

            await unitOfWork.SaveAsync();

            return ConditionStatus.Success;
        }
    }
}
