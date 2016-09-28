using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.ContentStorage.Azure.Models;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models.Elements.Medias;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// MediaFileHelper.
    /// </summary>
    public class MediaFileHelper : IMediaFileHelper
    {
        private IContentStorage contentStorage;
        private readonly ITagsService tagsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaFileHelper" /> class.
        /// </summary>
        /// <param name="contentStorage">The content storage.</param>
        /// <param name="tagsService">The tags service.</param>
        public MediaFileHelper(IContentStorage contentStorage, ITagsService tagsService)
        {
            this.contentStorage = contentStorage;
            this.tagsService = tagsService;
        }

        /// <summary>
        /// Creates the media file.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public async Task<Media> CreateMediaFile(BaseMediaRequestDto source)
        {
            var contentModel = Mapper.Map<BaseMediaRequestDto, ContentModel>(source);

            var uploadResult = await contentStorage.UploadContent(contentModel);

            if (uploadResult == null)
            {
                return await Task.FromResult<Media>(null);
            }

            var media = new Media
            {
                ContentLength = uploadResult.OriginalContentLength,
                OriginalStorageKey = uploadResult.OriginalStorageKey,
                ThumbnailStorageKey = uploadResult.ThumbnailStorageKey,
                OriginalFileName = uploadResult.OriginalFileName,
                CustomerId = CareElementRequestContext.Current.CustomerId,
                Tags = await tagsService.BuildTagsList(CareElementRequestContext.Current.CustomerId, source.Tags)
            };

            Mapper.Map<BaseMediaRequestDto, Media>(source, media);

            return media;
        }
    }
}