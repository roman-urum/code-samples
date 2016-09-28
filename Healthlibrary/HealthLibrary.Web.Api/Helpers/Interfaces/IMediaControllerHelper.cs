using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.Medias;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IMediaControllerHelper.
    /// </summary>
    public interface IMediaControllerHelper
    {
        /// <summary>
        /// Creates the specified media.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        Task<OperationResultDto<Guid, CreateMediaStatus>> CreateMedia(int customerId, CreateMediaRequestDto media);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <returns></returns>
        Task<MediaResponseDto> GetMedia(int customerId, Guid mediaId);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <returns></returns>
        Task<DeleteMediaStatus> DeleteMedia(int customerId, Guid mediaId);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <param name="updateMediaDto">The update media dto.</param>
        /// <returns></returns>
        Task<UpdateMediaStatus> UpdateMedia(
            int customerId,
            Guid mediaId, 
            UpdateMediaRequestDto updateMediaDto
        );

        /// <summary>
        /// Gets the medias.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<MediaResponseDto>> GetMedias(
            int customerId, 
            MediaSearchDto request
        );
    }
}