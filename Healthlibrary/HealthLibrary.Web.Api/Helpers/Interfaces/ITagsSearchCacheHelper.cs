using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ITagsSearchCacheHelper.
    /// </summary>
    public interface ITagsSearchCacheHelper
    {
        /// <summary>
        /// Gets all cached tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<HashSet<string>> GetAllCachedTags(int customerId);

        /// <summary>
        /// Adds the or update tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        Task AddOrUpdateTags(int customerId, IList<string> tags);

        /// <summary>
        /// Removes the tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        Task RemoveTags(int customerId, IList<string> tags);
    }
}