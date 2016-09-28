using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.ScaleAnswerSets;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IScaleAnswerSetHelper.
    /// </summary>
    public interface IScaleAnswerSetControllerHelper
    {
        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateScaleAnswerSetStatus>> Create(int customerId, CreateScaleAnswerSetRequestDto model);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        Task<ScaleAnswerSetResponseDto> Get(int customerId, Guid scaleAnswerSetId, string language);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<UpdateScaleAnswerSetStatus> Update(int customerId, Guid scaleAnswerSetId, UpdateScaleAnswerSetRequestDto model);

        /// <summary>
        /// Deletes Answerset with specified id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <returns></returns>
        Task<DeleteStatus> Delete(int customerId, Guid scaleAnswerSetId);

        /// <summary>
        /// Finds the specified model.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<PagedResultDto<ScaleAnswerSetResponseDto>> Find(int customerId, TagsSearchDto model = null);

        /// <summary>
        /// Updates the localized strings.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="scaleAnswerSetId">The scale answer set identifier.</param>
        /// <param name="model">The model.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        Task<UpdateScaleAnswerSetLocalization> UpdateLocalizedStrings(
            int customerId,
            Guid scaleAnswerSetId,
            UpdateScaleAnswerSetLocalizedRequestDto model,
            string language
        );
    }
}