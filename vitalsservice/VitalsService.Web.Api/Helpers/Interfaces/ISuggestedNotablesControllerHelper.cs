using System;
using System.Threading.Tasks;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.PatientNotes;

namespace VitalsService.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ISuggestedNotablesControllerHelper.
    /// </summary>
    public interface ISuggestedNotablesControllerHelper
    {
        /// <summary>
        /// Creates the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, SuggestedNotableStatus>> CreateSuggestedNotable(
            int customerId, 
            SuggestedNotableRequestDto request
        );

        /// <summary>
        /// Updates the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <param name="request">The request.</param>
        Task<SuggestedNotableStatus> UpdateSuggestedNotable(
            int customerId,
            Guid suggestedNotableId,
            SuggestedNotableRequestDto request
        );

        /// <summary>
        /// Deletes the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        Task<SuggestedNotableStatus> DeleteSuggestedNotable(
            int customerId,
            Guid suggestedNotableId
        );

        /// <summary>
        /// Gets the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        Task<OperationResultDto<SuggestedNotableDto, SuggestedNotableStatus>> GetSuggestedNotable(
            int customerId,
            Guid suggestedNotableId
        );

        /// <summary>
        /// Gets the suggested notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<SuggestedNotableDto>> GetSuggestedNotables(
            int customerId,
            BaseSearchDto request
        );
    }
}