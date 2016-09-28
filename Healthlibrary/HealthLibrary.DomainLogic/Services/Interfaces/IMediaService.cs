using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Domain.Entities.Element;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IMediaService.
    /// </summary>
    public interface IMediaService
    {
        /// <summary>
        /// Creates the specified media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        Task<Media> Create(Media media);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <returns></returns>
        Task<Media> Get(int customerId, Guid mediaId);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <returns></returns>
        Task<DeleteMediaStatus> Delete(int customerId, Guid mediaId);

        /// <summary>
        /// Updates the specified media.
        /// </summary>
        /// <param name="media">The media.</param>
        /// <returns></returns>
        Task Update(Media media);

        /// <summary>
        /// Gets the medias.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResult<Media>> GetMedias(int customerId, MediaSearchDto request);
    }
}