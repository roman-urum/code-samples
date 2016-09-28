using System.Collections.Generic;
using System.Threading.Tasks;

using VitalsService.Domain.DbEntities;

namespace VitalsService.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// ITagService.
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Finds the tags.
        /// </summary>
        /// <param name="tagNames">The tag names.</param>
        /// <returns></returns>
        Task<IList<Tag>> FindTags(IList<string> tagNames = null);

        /// <summary>
        /// Builds the tags list.
        /// </summary>
        /// <param name="tags">The tags.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<IList<Tag>> BuildTagsList(IList<string> tags, int customerId);

        /// <summary>
        /// Removes the unused tags.
        /// </summary>
        /// <returns>The list of removed Tags.</returns>
        Task<IList<string>> RemoveUnusedTags();
    }
}