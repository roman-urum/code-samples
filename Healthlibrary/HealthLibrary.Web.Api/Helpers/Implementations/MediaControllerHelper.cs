using System;
using System.Linq;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using AutoMapper;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Domain.Entities.Element;
using System.Threading.Tasks;
using HealthLibrary.Common.Extensions;
using HealthLibrary.ContentStorage.Azure.Models;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Dtos.Enums;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.Medias;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// MediaControllerHelper.
    /// </summary>
    public class MediaControllerHelper : IMediaControllerHelper
    {
        private readonly IMediaService mediaService;
        private readonly IContentStorage contentStorage;
        private readonly ITagsService tagsService;
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaControllerHelper" /> class.
        /// </summary>
        /// <param name="mediaService">The media service.</param>
        /// <param name="contentStorage">The content storage.</param>
        /// <param name="tagsService">The tags service.</param>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        public MediaControllerHelper(
            IMediaService mediaService,
            IContentStorage contentStorage,
            ITagsService tagsService,
            ITagsSearchCacheHelper tagsSearchCacheHelper
        )
        {
            this.mediaService = mediaService;
            this.contentStorage = contentStorage;
            this.tagsService = tagsService;
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
        }

        /// <summary>
        /// Creates the specified media dto.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaDto">The media dto.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, CreateMediaStatus>> CreateMedia(int customerId, CreateMediaRequestDto mediaDto)
        {
            var contentModel = Mapper.Map<CreateMediaRequestDto, ContentModel>(mediaDto);

            var uploadResult = await contentStorage.UploadContent(contentModel);

            if (uploadResult == null)
            {
                return new OperationResultDto<Guid, CreateMediaStatus>()
                {
                    Status = CreateMediaStatus.InvalidContentOrSourceContentUrlProvided
                };
            }

            var media = new Media()
            {
                CustomerId = customerId,
                Name = mediaDto.Name,
                Description = mediaDto.Description,
                ContentType = mediaDto.ContentType,
                ContentLength = uploadResult.OriginalContentLength,
                Tags = await tagsService.BuildTagsList(customerId, mediaDto.Tags),
                OriginalStorageKey = uploadResult.OriginalStorageKey,
                ThumbnailStorageKey = uploadResult.ThumbnailStorageKey,
                OriginalFileName = uploadResult.OriginalFileName
            };

            var createdMedia = await mediaService.Create(media);

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, createdMedia.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return new OperationResultDto<Guid, CreateMediaStatus>()
            {
                Content = createdMedia.Id,
                Status = CreateMediaStatus.Success
            };
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <returns></returns>
        public async Task<MediaResponseDto> GetMedia(int customerId, Guid mediaId)
        {
            var media = await mediaService.Get(customerId, mediaId);

            var mediaResponseDto = Mapper.Map<Media, MediaResponseDto>(media);

            return mediaResponseDto;
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The identifier.</param>
        /// <returns></returns>
        public async Task<DeleteMediaStatus> DeleteMedia(int customerId, Guid mediaId)
        {
            var result = await mediaService.Delete(customerId, mediaId);

            if (result == DeleteMediaStatus.Success)
            {
                var unusedTags = await tagsService.RemoveUnusedTags(customerId);
                await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);
            }

            return result;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="mediaId">The identifier.</param>
        /// <param name="updateMediaDto">The update media dto.</param>
        /// <returns></returns>
        public async Task<UpdateMediaStatus> UpdateMedia(
            int customerId, 
            Guid mediaId,
            UpdateMediaRequestDto updateMediaDto
        )
        {
            var media = await mediaService.Get(customerId, mediaId);

            if (media == null)
            {
                return UpdateMediaStatus.NotFound;
            }

            if (!string.IsNullOrEmpty(updateMediaDto.Content) ||
                !string.IsNullOrEmpty(updateMediaDto.SourceContentUrl))
            {
                var contentModel = Mapper.Map<UpdateMediaRequestDto, ContentModel>(updateMediaDto);

                var uploadResult = await contentStorage.UploadContent(contentModel);

                if (uploadResult == null)
                {
                    return UpdateMediaStatus.InvalidContentOrSourceContentUrlProvided;
                }

                media.OriginalStorageKey = uploadResult.OriginalStorageKey;
                media.ThumbnailStorageKey = uploadResult.ThumbnailStorageKey;
                media.ContentLength = uploadResult.OriginalContentLength;
                media.OriginalFileName = uploadResult.OriginalFileName;
            }

            media.Name = updateMediaDto.Name;
            media.Description = updateMediaDto.Description;
            media.ContentType = updateMediaDto.ContentType;

            media.Tags.RemoveRange(media.Tags.ToList());

            var newElementTags = await tagsService.BuildTagsList(customerId, updateMediaDto.Tags);
            media.Tags.AddRange(newElementTags);

            await mediaService.Update(media);

            await tagsSearchCacheHelper.AddOrUpdateTags(customerId, media.Tags.Select(t => t.Name).ToList());

            var unusedTags = await tagsService.RemoveUnusedTags(customerId);
            await tagsSearchCacheHelper.RemoveTags(customerId, unusedTags);

            return UpdateMediaStatus.Success;
        }

        /// <summary>
        /// Gets the medias.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<MediaResponseDto>> GetMedias(
            int customerId, 
            MediaSearchDto request
        )
        {
            var medias = await mediaService.GetMedias(customerId, request);

            return Mapper.Map<PagedResult<Media>, PagedResultDto<MediaResponseDto>>(medias);
        }
    }
}