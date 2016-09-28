using System;
using System.Threading.Tasks;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// ICacheProvider.
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Adds value to cache under the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void Add(string key, object value);

        /// <summary>
        /// Removes value from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        T Get<T>(string key, Func<T> action = null) where T : class;

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        Task<T> Get<T>(string key, Func<Task<T>> action = null) where T : class;

        /// <summary>
        /// Clears cache.
        /// </summary>
        void Clear();
    }
}