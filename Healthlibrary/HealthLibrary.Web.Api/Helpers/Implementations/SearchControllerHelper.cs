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
    /// SearchControllerHelper.
    /// </summary>
    public class SearchControllerHelper : ISearchControllerHelper
    {
        private readonly IGlobalSearchCacheHelper globalSearchCacheHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchControllerHelper" /> class.
        /// </summary>
        /// <param name="globalSearchCacheHelper">The global search cache helper.</param>
        public SearchControllerHelper(IGlobalSearchCacheHelper globalSearchCacheHelper)
        {
            this.globalSearchCacheHelper = globalSearchCacheHelper;
        }

        /// <summary>
        /// Searches the specified search request.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<SearchEntryDto>> Search(int customerId, GlobalSearchDto searchRequest)
        {
            var allCachedEntries = await globalSearchCacheHelper.GetAllCachedEntries(customerId);

            Expression<Func<SearchEntryDto, bool>> expression = PredicateBuilder.True<SearchEntryDto>();

            if (searchRequest != null)
            {
                if (searchRequest.Tags != null && searchRequest.Tags.Any())
                {
                    Expression<Func<SearchEntryDto, bool>> tagsExpression = PredicateBuilder.False<SearchEntryDto>(); ;

                    foreach (var tag in searchRequest.Tags)
                    {
                        tagsExpression = tagsExpression.Or(se => se.Tags.Any(t => t.ToLower() == tag.ToLower()));
                    }

                    expression = expression.And(tagsExpression);
                }

                if (!string.IsNullOrEmpty(searchRequest.Q))
                {
                    var terms = searchRequest.Q.Split(' ').Where(r => !string.IsNullOrWhiteSpace(r));

                    foreach (var term in terms)
                    {
                        expression = expression.And(se => se.Name.ToLower().Contains(term.ToLower()));
                    }
                }

                if (searchRequest.Categories != null && searchRequest.Categories.Any())
                {
                    Expression<Func<SearchEntryDto, bool>> innerCategoryExpression = PredicateBuilder.False<SearchEntryDto>();

                    foreach (var category in searchRequest.Categories)
                    {
                        innerCategoryExpression = innerCategoryExpression.Or(se => se.Type == category);
                    }

                    expression = expression.And(innerCategoryExpression);
                }
            }

            var filteredEntries = allCachedEntries
                .Values
                .Where(expression.Compile())
                .OrderBy(e => e.Type)
                .ThenBy(e => e.Name)
                .ToList();

            var total = filteredEntries.LongCount();

            if (searchRequest != null)
            {
                filteredEntries = filteredEntries.Skip(searchRequest.Skip).Take(searchRequest.Take).ToList();
            }

            return new PagedResultDto<SearchEntryDto>()
            {
                Results = filteredEntries,
                Total = total
            };
        }
    }
}