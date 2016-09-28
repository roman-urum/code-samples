using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Models;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ITagsControllerHelper.
    /// </summary>
    public interface ITagsControllerHelper
    {
        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<string>> SearchTags(int customerId, BaseSearchDto request);
    }
}