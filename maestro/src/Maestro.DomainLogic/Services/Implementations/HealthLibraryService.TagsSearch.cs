using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// HealthLibraryService.TagsSearch
    /// </summary>
    public partial class HealthLibraryService
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        public async Task<IList<string>> SearchTags(string token, int customerId, string term)
        {
            var result = await healthLibraryDataProvider.SearchTags(token, customerId, term);

            return result;
        }
    }
}