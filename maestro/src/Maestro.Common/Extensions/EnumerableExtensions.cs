using System;
using System.Collections.Generic;
using System.Linq;

namespace Maestro.Common.Extensions
{
    /// <summary>
    /// Extension methods for collections.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines if collection contains all elements in specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> target, T[] items)
        {
            return items.All(target.Contains);
        }

        /// <summary>
        /// Splits an array into several smaller arrays.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="source">The array to split.</param>
        /// <param name="size">The size of the smaller arrays.</param>
        /// <returns>An array containing smaller arrays.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int size)
        {
            return source.Select((s, i) => new { Value = s, Index = i })
                     .GroupBy(item => item.Index / size, item => item.Value)
                     .Cast<IEnumerable<T>>();
        }

        /// <summary>
        /// Eaches the specified action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (T item in items)
            {
                action(item);
            }
        }
    }
}