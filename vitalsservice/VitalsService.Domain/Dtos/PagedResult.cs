using System.Collections.Generic;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// PagedResult.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T> where T : class
    {
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        public IList<T> Results { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public long Total { get; set; }
    }
}