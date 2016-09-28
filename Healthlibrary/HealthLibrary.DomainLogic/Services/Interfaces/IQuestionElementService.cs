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
    /// Contains business rules to manage question elements.
    /// </summary>
    public interface IQuestionElementService
    {
        /// <summary>
        /// Creates new instance of Question element in database.
        /// </summary>
        /// <param name="questionElement"></param>
        /// <returns></returns>
        Task<ServiceActionResult<QuestionElementActionStatus, QuestionElement>> Create(QuestionElement questionElement);

        /// <summary>
        /// Returns required answerset by id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        Task<QuestionElement> Get(int customerId, Guid questionElementId);

        /// <summary>
        /// Updates data of question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="questionElement">The question element.</param>
        /// <returns></returns>
        Task<ServiceActionResult<QuestionElementActionStatus, QuestionElement>> Update(
            int customerId,
            Guid questionElementId,
            QuestionElement questionElement
        );

        /// <summary>
        /// Updates data of question element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <param name="localizedString">The localized string.</param>
        /// <returns></returns>
        Task<QuestionElement> UpdateLocalizedString(
            int customerId, 
            Guid questionElementId,
            QuestionElementString localizedString
        );

        /// <summary>
        /// Marks the item as deleted.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="questionElementId">The question element identifier.</param>
        /// <returns></returns>
        Task<DeleteStatus> Delete(int customerId, Guid questionElementId);

        /// <summary>
        /// Returns question elements by criteria.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        Task<PagedResult<QuestionElement>> Find(int customerId, TagsSearchDto searchRequest = null);
    }
}
