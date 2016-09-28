using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using EntityFramework.Extensions;
using EntityFramework.Future;

namespace VitalsService.DomainLogic.Tests.Helpers
{
    public static class EntityFrameworkExtendedHelper
    {
        public static FutureValue<TEntity> FutureFirstOrDefault<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            return (query.ToObjectQuery() == null)
                ? new FutureValue<TEntity>(query.FirstOrDefault())
                : FutureExtensions.FutureFirstOrDefault(query);
        }

        public static FutureQuery<TEntity> Future<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            if (query.ToObjectQuery() != null)
                return FutureExtensions.Future(query);

            var args = new[] { (object)query, (object)null };
            var obj = (FutureQuery<TEntity>)Activator
                .CreateInstance(typeof(FutureQuery<TEntity>), BindingFlags.NonPublic | BindingFlags.Instance, null, args, null);

            return obj;
        }

        public static FutureCount FutureCount<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            return (query.ToObjectQuery() == null)
                ? new FutureCount(query.Count())
                : FutureExtensions.FutureCount(query);
        }
    }
}
