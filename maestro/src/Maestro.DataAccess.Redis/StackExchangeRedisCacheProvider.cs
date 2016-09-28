using System;
using System.Threading.Tasks;
using Maestro.DataAccess.Redis.Extensions;
using StackExchange.Redis;

namespace Maestro.DataAccess.Redis
{
    /// <summary>
    /// StackExchangeRedisCacheProvider.
    /// </summary>
    public class StackExchangeRedisCacheProvider : ICacheProvider
    {
        private readonly IDatabase database;

        private const string EmptyResultValue = "EmptyResult";

        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisCacheProvider" /> class.
        /// </summary>
        /// <param name="database">The database.</param>
        public StackExchangeRedisCacheProvider(IDatabase database)
        {
            this.database = database;
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            return database.KeyExists(key);
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <param name="dependsOnKey">The depends on key.</param>
        public void Add(
            string key,
            object value,
            TimeSpan? expiry = null,
            string dependsOnKey = null
        )
        {
            // Lets not store the base type (will be dependsOnKey later) since we want to use it as a set!
            if (Equals(value, string.Empty))
            {
                return;
            }

            var primaryAdded = database.Set(key, value, expiry);

            if (dependsOnKey != null && primaryAdded)
            {
                database.SetAdd(dependsOnKey, key);
            }
        }

        /// <summary>
        /// Gets value using specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="action">The action.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public T Get<T>(
            string key, 
            Func<T> action = null, 
            TimeSpan? expiry = null
        ) where T : class
        {
            T result = null;

            var preRes = database.Get<T>(key);

            if (preRes == null)
            {
                if (action != null)
                {
                    T value = action();

                    if (value == null)
                    {
                        Add(key, EmptyResultValue, expiry);
                    }
                    else
                    {
                        Add(key, value, expiry);
                    }

                    result = value;
                }
            }
            else
            {
                if (!(preRes is string && preRes as string == EmptyResultValue))
                {
                    result = preRes;
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
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public async Task<T> Get<T>(
            string key,
            Func<Task<T>> action = null, 
            TimeSpan? expiry = null
        ) where T : class
        {
            T result = null;

            var preRes = database.Get<T>(key);

            if (preRes == null)
            {
                if (action != null)
                {
                    T value = await action();

                    if (value == null)
                    {
                        Add(key, EmptyResultValue, expiry);
                    }
                    else
                    {
                        Add(key, value, expiry);
                    }

                    result = value;
                }
            }
            else
            {
                if (!(preRes is string && preRes as string == EmptyResultValue))
                {
                    result = preRes;
                }
            }

            return result;
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            database.KeyDelete(key);
        }

        /// <summary>
        /// Removes keys by pattern.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        public void RemoveByPattern(string pattern)
        {
            var endPoints = database.Multiplexer.GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                var server = database.Multiplexer.GetServer(endPoint);

                foreach (var key in server.Keys(pattern: pattern))
                {
                    Remove(key);
                }
            }
        }
    }
}