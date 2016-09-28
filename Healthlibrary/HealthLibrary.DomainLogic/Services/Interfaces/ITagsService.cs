using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Domain.Entities;

namespace HealthLibrary.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// ITagsService.
    /// </summary>
    public interface ITagsService
    {
        /// <summary>
        /// Finds the tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="tagNames">The tag names.</param>
        /// <returns></returns>
        Task<IList<Tag>> FindTags(int customerId, IList<string> tagNames = null);

        /// <summary>
        /// Builds the tags list.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        Task<IList<Tag>> BuildTagsList(int customerId, IList<string> tags);

        /// <summary>
        /// Removes the unused tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>
        /// The list of removed Tags.
        /// </returns>
        Task<IList<string>> RemoveUnusedTags(int customerId);
    }
}