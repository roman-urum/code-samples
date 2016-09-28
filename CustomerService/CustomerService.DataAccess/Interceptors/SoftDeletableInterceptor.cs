using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using CustomerService.Domain;
using Isg.EntityFramework.Interceptors;

namespace CustomerService.DataAccess.Interceptors
{
    /// <summary>
    /// Soft delete interceptor.
    /// </summary>
    public class SoftDeletableInterceptor : ChangeInterceptor<ISoftDelitable>
    {
        /// <summary>
        /// Called before entity delete.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="item">The item.</param>
        /// <param name="context">The context.</param>
        /// <exception cref="System.InvalidOperationException">Item is already deleted.</exception>
        protected override void OnBeforeDelete(DbEntityEntry entry, ISoftDelitable item, InterceptionContext context)
        {
            if (item.IsDeleted)
            {
                throw new InvalidOperationException("Item is already deleted.");
            }

            base.OnBeforeDelete(entry, item, context);
            item.IsDeleted = true;
            entry.State = EntityState.Modified;
        }
    }
}