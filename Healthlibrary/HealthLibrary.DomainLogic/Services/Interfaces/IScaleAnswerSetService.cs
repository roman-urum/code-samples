using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    public interface IScaleAnswerSetService
    {
        /// <summary>
        /// Creates the specified answer set.
        /// </summary>
        /// <param name="answerSet">The answer set.</param>
        Task<ScaleAnswerSet> Create(ScaleAnswerSet answerSet);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        Task<ScaleAnswerSet> Get(int customerId, Guid scaleAnswerSetId);

        /// <summary>
        /// Updates the specified answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="answerSet">The answer set.</param>
        /// <returns></returns>
        Task<UpdateScaleAnswerSetStatus> Update(int customerId, Guid scaleAnswerSetId, ScaleAnswerSet answerSet);

        /// <summary>
        /// Sets specified entity as deleted in db.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        Task<DeleteStatus> Delete(int customerId, Guid scaleAnswerSetId);

        /// <summary>
        /// Finds the specified keyword.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResult<ScaleAnswerSet>> Find(int customerId, TagsSearchDto searchRequest = null);

        /// <summary>
        /// Updates only labels for Answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="answerSetId">The answer set identifier.</param>
        /// <param name="lowLabel">The low label.</param>
        /// <param name="midLabel">The mid label.</param>
        /// <param name="highLabel">The high label.</param>
        /// <returns></returns>
        Task<UpdateScaleAnswerSetLocalization> UpdateLabels(
            int customerId,
            Guid answerSetId,
            LowLabelScaleAnswerSetString lowLabel,
            MidLabelScaleAnswerSetString midLabel, 
            HighLabelScaleAnswerSetString highLabel
        );

        /// <summary>
        /// Gets for customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<ScaleAnswerSet> GetForCustomer(Guid id, int customerId);
    }
}
