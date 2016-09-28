using System;
using System.Collections.Generic;
using System.Linq;
using Maestro.DataAccess.Redis.Extensions;
using StackExchange.Redis;
using WebApi.OutputCache.Core.Cache;

namespace Maestro.Web.Helpers
{
    /// <summary>
    /// StackExchangeRedisOutputCacheProvider.
    /// </summary>
    public class StackExchangeRedisOutputCacheProvider : IApiOutputCache
    {
        private readonly IDatabase redisCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackExchangeRedisOutputCacheProvider" /> class.
        /// </summary>
        /// <param name="redisCache">The redis cache.</param>
        public StackExchangeRedisOutputCacheProvider(IDatabase redisCache)
        {
            this.redisCache = redisCache;
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <value>
        /// All keys.
        /// </value>
        public IEnumerable<string> AllKeys
        {
            get
            {
                foreach (string key in redisCache.Multiplexer.GetAllKeys())
                {
                    yield return key;
                }
            }
        }

        /// <summary>
        /// Removes the starts with.
        /// </summary>
        /// <param name="key">The key.</param>
        public void RemoveStartsWith(string key)
        {
            var endPoints = redisCache.Multiplexer.GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                var server = redisCache.Multiplexer.GetServer(endPoint);

                var keys = server
                    .Keys(pattern: string.Format("{0}*", key))
                    .ToList();

                foreach (var memberKey in keys)
                {
                    Remove(memberKey);
                }
            }
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            return redisCache.Get<T>(key);
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return redisCache.Get(key);
        }

        /// <summary>
        /// Removes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            redisCache.KeyDelete(key);
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            var endPoints = redisCache.Multiplexer.GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                var server = redisCache.Multiplexer.GetServer(endPoint);

                var keys = server
                    .Keys(pattern: string.Format("{0}*", key))
                    .ToList();

                if (keys.Any())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The expiration.</param>
        /// <param name="dependsOnKey">The depends on key.</param>
        public void Add(string key, object value, DateTimeOffset expiration, string dependsOnKey = null)
        {
            // Lets not store the base type (will be dependsOnKey later) since we want to use it as a set!
            if (Equals(value, string.Empty))
            {
                return;
            }

            var primaryAdded = redisCache.Set(key, value, expiration.Subtract(DateTimeOffset.Now));

            if (dependsOnKey != null && primaryAdded)
            {
                redisCache.SetAdd(dependsOnKey, key);
            }
        }
    }
}