using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maestro.Web.Areas.Customer.Managers.Interfaces
{
    /// <summary>
    /// ICareBuilderControllerManager.TagsSearch
    /// </summary>
    public partial interface ICareBuilderControllerManager
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <returns></returns>
        Task<IList<string>> SearchTags(string term);
    }
}