using System;

namespace Maestro.Web.Extensions
{
    /// <summary>
    /// DateTimeExtensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Greaters the or equal than.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool GreaterOrEqualThan(this DateTime? target, DateTime? date)
        {
            return (!target.HasValue && !date.HasValue) ||
                   (target.HasValue && !date.HasValue) ||
                   (target.HasValue && target.Value >= date.Value);
        }

        /// <summary>
        /// Lesses the or equal than.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static bool LessOrEqualThan(this DateTime? target, DateTime? date)
        {
            return (!target.HasValue && !date.HasValue) ||
                   (target.HasValue && !date.HasValue) ||
                   (target.HasValue && target.Value <= date.Value);
        }
    }
}