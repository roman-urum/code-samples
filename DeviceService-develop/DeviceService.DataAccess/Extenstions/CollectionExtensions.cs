using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DeviceService.DataAccess.Estenstions
{
    /// <summary>
    /// Extension methods for <see cref="Collection{T}"/>.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="T:System.Collections.Generic.List`1" />.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="list"><see cref="ICollection{T}" /> target collection.</param>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="T:System.Collections.Generic.List`1" />.
        /// The collection itself cannot be null, but it can contain elements that are null,  if type T is a reference type.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection" /> is null.</exception>
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> collection)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var obj in collection)
            {
                list.Add(obj);
            }
        }

        /// <summary>
        /// Removes elements from the specified collection.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="list"><see cref="ICollection{T}" /> target collection.</param>
        /// <param name="collection">The collection whose elements should be removed.
        /// The collection itself cannot be null, but it can contain elements that are null,  if type T is a reference type.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="collection" /> is null.</exception>
        public static void RemoveRange<T>(this ICollection<T> list, IEnumerable<T> collection)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var obj in collection)
            {
                list.Remove(obj);
            }
        }
    }
}
