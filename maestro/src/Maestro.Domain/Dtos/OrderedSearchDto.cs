using System;
using Maestro.Common.ApiClient;
using Maestro.Domain.Enums;

namespace Maestro.Domain.Dtos
{
    /// <summary>
    /// OrderedSearchDto.
    /// </summary>
    /// <typeparam name="TOrder">The type of the order.</typeparam>
    /// <seealso cref="Maestro.Domain.Dtos.BaseSearchDto" />
    public class OrderedSearchDto<TOrder> : BaseSearchDto 
        where TOrder : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public TOrder OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public SortDirection SortDirection { get; set; }
    }
}