using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace VitalsService.Web.Api.Extensions
{
    /// <summary>
    /// StackExchangeRedisExtensions.
    /// </summary>
    public static class StackExchangeRedisExtensions
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T Get<T>(this IDatabase cache, string key)
        {
            RedisValue result = cache.StringGet(key);

            if (result.IsNull)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(result.ToString());
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static object Get(this IDatabase cache, string key)
        {
            return JsonConvert.DeserializeObject<object>(cache.StringGet(key).ToString());
        }

        /// <summary>
        /// Gets all keys.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <returns></returns>
        public static IEnumerable<RedisKey> GetAllKeys(this ConnectionMultiplexer connectionMultiplexer)
        {
            var keys = new HashSet<RedisKey>();

            //Could have more than one instance https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md

            var endPoints = connectionMultiplexer.GetEndPoints();

            foreach (EndPoint endpoint in endPoints)
            {
                var dbKeys = connectionMultiplexer.GetServer(endpoint).Keys();

                foreach (var dbKey in dbKeys)
                {
                    if (!keys.Contains(dbKey))
                    {
                        keys.Add(dbKey);
                    }
                }
            }

            return keys;
        }

        /// <summary>
        /// Searches the keys.
        /// </summary>
        /// <param name="connectionMultiplexer">The connection multiplexer.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public static IEnumerable<RedisKey> SearchKeys(this ConnectionMultiplexer connectionMultiplexer, string searchPattern)
        {
            var keys = new HashSet<RedisKey>();

            //Could have more than one instance https://github.com/StackExchange/StackExchange.Redis/blob/master/Docs/KeysScan.md

            var endPoints = connectionMultiplexer.GetEndPoints();

            foreach (EndPoint endpoint in endPoints)
            {
                var dbKeys = connectionMultiplexer.GetServer(endpoint).Keys(pattern: searchPattern);

                foreach (var dbKey in dbKeys)
                {
                    if (!keys.Contains(dbKey))
                    {
                        keys.Add(dbKey);
                    }
                }
            }

            return keys;
        }

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns></returns>
        public static bool Set(this IDatabase cache, string key, object value, TimeSpan expiry)
        {
            return cache.StringSet(key, JsonConvert.SerializeObject(value), expiry);
        }
    }
}