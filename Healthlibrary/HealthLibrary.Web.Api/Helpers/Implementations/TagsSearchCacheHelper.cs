using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Common.Extensions;
using HealthLibrary.Domain.Entities;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// TagsSearchCacheHelper.
    /// </summary>
    public class TagsSearchCacheHelper : ITagsSearchCacheHelper
    {
        private const string TagsIndexKeyTemplate = "TAGS-INDEX-{0}";

        private readonly ICacheProvider cacheProvider;
        private readonly ICareElementRequestContext careElementRequestContext;
        private readonly ITagsService tagsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsSearchCacheHelper" /> class.
        /// </summary>
        /// <param name="cacheProvider">The cache provider.</param>
        /// <param name="careElementRequestContext">The care element request context.</param>
        /// <param name="tagsService">The tags service.</param>
        public TagsSearchCacheHelper(
            ICacheProvider cacheProvider,
            ICareElementRequestContext careElementRequestContext,
            ITagsService tagsService
        )
        {
            this.cacheProvider = cacheProvider;
            this.careElementRequestContext = careElementRequestContext;
            this.tagsService = tagsService;
        }

        /// <summary>
        /// Gets all cached tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public async Task<HashSet<string>> GetAllCachedTags(int customerId)
        {
            var cacheKey = string.Format(TagsIndexKeyTemplate, customerId);

            var allCachedTags =
                await cacheProvider.Get<HashSet<string>>(
                    cacheKey,
                    async () => new HashSet<string>(Mapper.Map<IList<Tag>, IList<string>>(await tagsService.FindTags(customerId)))
                );

            return allCachedTags;
        }

        /// <summary>
        /// Adds the or update tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        public async Task AddOrUpdateTags(int customerId, IList<string> tags)
        {
            var cachedTags = await GetAllCachedTags(customerId);

            cachedTags.AddRange(tags);
        }

        /// <summary>
        /// Removes the tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        public async Task RemoveTags(int customerId, IList<string> tags)
        {
            var cachedTags = await GetAllCachedTags(customerId);

            cachedTags.RemoveRange(tags);
        }
    }
}