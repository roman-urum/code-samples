using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Web.Areas.Customer.Managers.Implementations
{
    /// <summary>
    /// CareBuilderControllerManager.TagsSearch
    /// </summary>
    public partial class CareBuilderControllerManager
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        public async Task<IList<string>> SearchTags(string term)
        {
            var token = authDataStorage.GetToken();

            var resuts = await healthLibraryService.SearchTags(token, CustomerContext.Current.Customer.Id, term);
           
            return resuts;
        }
    }
}