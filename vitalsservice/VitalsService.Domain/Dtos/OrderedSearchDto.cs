using System;
using VitalsService.Domain.Enums;

namespace VitalsService.Domain.Dtos
{
    /// <summary>
    /// OrderedSearchDto.
    /// </summary>
    /// <typeparam name="TOrder">The type of the order.</typeparam>
    /// <seealso cref="VitalsService.Domain.Dtos.BaseSearchDto" />
    public class OrderedSearchDto<TOrder> : BaseSearchDto 
        where TOrder : struct, IComparable, IConvertible, IFormattable
    {
        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>
        /// The order by.
        /// </value>
        public TOrder OrderBy { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public SortDirection SortDirection { get; set; }
    }
}