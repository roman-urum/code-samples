using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Results;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// Contains business rules to work with selection answer-sets.
    /// </summary>
    public interface ISelectionAnswerSetService
    {
        /// <summary>
        /// Creates new answerset with answers.
        /// </summary>
        /// <param name="answerSet">Answer-set data</param>
        Task<SelectionAnswerSet> Create(SelectionAnswerSet answerSet);

        /// <summary>
        /// Updates existed answerset with new default answer strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="answerSet">The answer set.</param>
        /// <returns></returns>
        Task<ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSet>> Update(
            int customerId,
            Guid selectionAnswerSetId,
            SelectionAnswerSet answerSet
        );

        /// <summary>
        /// Updates answerset with additional translations for answer choices.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="answerChoices">The answer choices.</param>
        /// <returns></returns>
        Task<ServiceActionResult<SelectionAnswerSet>> UpdateLocalizedStrings(
            int customerId,
            Guid selectionAnswerSetId,
            IDictionary<Guid, SelectionAnswerChoiceString> answerChoices
        );

        /// <summary>
        /// Sets specified entity as deleted in db.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        Task<DeleteStatus> Delete(int customerId, Guid selectionAnswerSetId);

        /// <summary>
        /// Returns answer set by answerset id and customer id.
        /// Customer id should be null for generic answer sets.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        Task<SelectionAnswerSet> Get(int customerId, Guid selectionAnswerSetId);

        /// <summary>
        /// Returns list of selection sets which matches to provided search criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResult<SelectionAnswerSet>> Find(int customerId, SelectionAnswerSetSearchDto searchRequest = null);
    }
}
