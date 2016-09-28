using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HealthLibrary.Common.Helpers;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// TagsControllerHelper.
    /// </summary>
    public class TagsControllerHelper : ITagsControllerHelper
    {
        private readonly ITagsSearchCacheHelper tagsSearchCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagsControllerHelper" /> class.
        /// </summary>
        /// <param name="tagsSearchCacheHelper">The tags search cache helper.</param>
        public TagsControllerHelper(ITagsSearchCacheHelper tagsSearchCacheHelper)
        {
            this.tagsSearchCacheHelper = tagsSearchCacheHelper;
        }

        /// <summary>
        /// Searches the tags.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<string>> SearchTags(int customerId, BaseSearchDto request)
        {
            var allCachedTags = await tagsSearchCacheHelper.GetAllCachedTags(customerId);

            Expression<Func<string, bool>> expression = PredicateBuilder.True<string>();

            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Q))
                {
                    var terms = request.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var t in terms)
                    {
                        expression = expression.And(e => e.ToLower().Contains(t.ToLower()));
                    }
                }
            }

            var filteredTags = allCachedTags
                .Where(expression.Compile())
                .OrderBy(t => t)
                .ToList();

            var total = filteredTags.LongCount();

            if (request != null)
            {
                filteredTags = filteredTags.Skip(request.Skip).Take(request.Take).ToList();
            }

            return new PagedResultDto<string>()
            {
                Results = filteredTags,
                Total = total
            };
        }
    }
}