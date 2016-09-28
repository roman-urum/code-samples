using System;
using System.Threading.Tasks;

namespace Maestro.DataAccess.Redis
{
    /// <summary>
    /// ICacheProvider.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// Adds value to cache under the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <param name="dependsOnKey">The depends on key.</param>
        void Add(string key, object value, TimeSpan? expiry = null, string dependsOnKey = null);

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        T Get<T>(string key, Func<T> action = null, TimeSpan? expiry = null) where T : class;

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        Task<T> Get<T>(string key, Func<Task<T>> action = null, TimeSpan? expiry = null) where T : class;

        /// <summary>
        /// Removes value from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Removes keys by pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        void RemoveByPattern(string pattern);
    }
}