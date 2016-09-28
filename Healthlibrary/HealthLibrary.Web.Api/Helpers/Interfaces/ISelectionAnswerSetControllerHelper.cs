using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.DomainLogic.Services.Results;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.SelectionAnswerSets;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Contains help methods to handle selection answer-set requests.
    /// </summary>
    public interface ISelectionAnswerSetControllerHelper
    {
        /// <summary>
        /// Creates entity and saves data in datasource.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Id of created entity
        /// </returns>
        Task<Guid> Create(int customerId, CreateSelectionAnswerSetRequestDto model);

        /// <summary>
        /// Updates existed answer set with new data.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ServiceActionResult<SelectionAnswerSetUpdateResult, SelectionAnswerSetResponseDto>> Update(
            int customerId,
            Guid selectionAnswerSetId, 
            UpdateSelectionAnswerSetRequestDto model
        );

        /// <summary>
        /// Updates existed answer set with new localized strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<ServiceActionResult<SelectionAnswerSetResponseDto>> UpdateLocalizedStrings(
            int customerId,
            Guid selectionAnswerSetId,
            UpdateSelectionAnswerSetLocalizedRequestDto model
        );

        /// <summary>
        /// Deletes Answerset with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <returns></returns>
        Task<DeleteStatus> Delete(int customerId, Guid selectionAnswerSetId);

        /// <summary>
        /// Returns answerset with answers for specified culture.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        Task<SelectionAnswerSetResponseDto> Get(int customerId, Guid selectionAnswerSetId, string language);

        /// <summary>
        /// Returns model with search results.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<PagedResultDto<SelectionAnswerSetResponseDto>> Find(
            int customerId,
            SelectionAnswerSetSearchDto criteria
        );
    }
}
