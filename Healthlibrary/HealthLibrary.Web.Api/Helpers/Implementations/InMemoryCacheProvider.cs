using System;
using System.Collections;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using HealthLibrary.Web.Api.Helpers.Interfaces;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// InMemoryCacheProvider.
    /// </summary>
    public class InMemoryCacheProvider : ICacheProvider
    {
        private const string EmptyResultValue = "EmptyResult";

        private static readonly Cache cache = HttpContext.Current.Cache;

        /// <summary>
        /// Adds value to cache under the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, object value)
        {
            cache.Insert(
                key,
                value,
                null,
                Cache.NoAbsoluteExpiration,
                new TimeSpan(30, 0, 0, 0),
                CacheItemPriority.Normal,
                null
            );
        }

        /// <summary>
        /// Removes value from the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            cache.Remove(key);
        }

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public T Get<T>(string key, Func<T> action = null) where T : class
        {
            T result = null;

            var preRes = cache.Get(key);

            if (preRes == null)
            {
                if (action != null)
                {
                    T value = action();

                    if (value == null)
                    {
                        Add(key, EmptyResultValue);
                    }
                    else
                    {
                        Add(key, value);
                    }

                    result = value;
                }
            }
            else
            {
                if (!(preRes is string && (string)preRes == EmptyResultValue))
                {
                    result = preRes as T;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(string key, Func<Task<T>> action = null) where T : class
        {
            T result = null;

            var preRes = cache.Get(key);

            if (preRes == null)
            {
                if (action != null)
                {
                    T value = await action();

                    if (value == null)
                    {
                        Add(key, EmptyResultValue);
                    }
                    else
                    {
                        Add(key, value);
                    }

                    result = value;
                }
            }
            else
            {
                if (!(preRes is string && (string)preRes == EmptyResultValue))
                {
                    result = preRes as T;
                }
            }

            return result;
        }

        /// <summary>
        /// Clears cache.
        /// </summary>
        public void Clear()
        {
            IDictionaryEnumerator enumerator = cache.GetEnumerator();

            while (enumerator.MoveNext())
            {
                var key = enumerator.Key as string;

                if (key != null)
                {
                    cache.Remove(key);
                }
            }
        }
    }
}