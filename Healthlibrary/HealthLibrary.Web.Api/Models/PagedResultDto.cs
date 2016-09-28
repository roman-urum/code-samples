using System.Collections.Generic;

namespace HealthLibrary.Web.Api.Models
{
    /// <summary>
    /// PagedResultDto.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResultDto<T> where T : class
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

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count {
            get
            {
                return Results.Count;
            }
        }
    }
}