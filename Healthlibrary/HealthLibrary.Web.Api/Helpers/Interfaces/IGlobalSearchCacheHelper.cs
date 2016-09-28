using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthLibrary.Web.Api.Models;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IGlobalSearchCacheHelper.
    /// </summary>
    public interface IGlobalSearchCacheHelper
    {
        /// <summary>
        /// Gets all cached entries.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Task<ConcurrentDictionary<Guid, SearchEntryDto>> GetAllCachedEntries(int customerId);

        /// <summary>
        /// Tries to update entry.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        Task AddOrUpdateEntry(int customerId, SearchEntryDto entry);

        /// <summary>
        /// Removes the entry.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="entryId">The entry identifier.</param>
        /// <returns></returns>
        Task RemoveEntry(int customerId, Guid entryId);
    }
}