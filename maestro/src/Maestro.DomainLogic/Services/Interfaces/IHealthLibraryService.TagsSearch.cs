using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IHealthLibraryService.TagsSearch
    /// </summary>
    public partial interface IHealthLibraryService
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        Task<IList<string>> SearchTags(string token, int customerId, string term);
    }
}