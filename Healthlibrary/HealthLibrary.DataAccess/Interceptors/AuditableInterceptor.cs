using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HealthLibrary.Domain;

using Isg.EntityFramework.Interceptors;

namespace HealthLibrary.DataAccess.Interceptors
{
    /// <summary>
    /// Audit interceptor
    /// </summary>
    public class AuditableInterceptor : ChangeInterceptor<IAuditable>
    {
        /// <summary>
        /// Called before entity insert.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="item">The item.</param>
        /// <param name="context">The context.</param>
        protected override void OnBeforeInsert(DbEntityEntry entry, IAuditable item, InterceptionContext context)
        {
            var now = DateTime.UtcNow;

            item.CreatedUtc = now;
            item.UpdatedUtc = now;

            base.OnBeforeInsert(entry, item, context);
        }

        /// <summary>
        /// Called before entity update.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="item">The item.</param>
        /// <param name="context">The context.</param>
        protected override void OnBeforeUpdate(DbEntityEntry entry, IAuditable item, InterceptionContext context)
        {
            item.UpdatedUtc = DateTime.UtcNow;

            base.OnBeforeUpdate(entry, item, context);
        }
    }
}
