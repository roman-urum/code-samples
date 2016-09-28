using System.Data.Entity.Infrastructure;
using Isg.EntityFramework.Interceptors;
using VitalsService.Domain;
using VitalsService.Helpers;

namespace VitalsService.DataAccess.EF.Interceptors
{
    /// <summary>
    /// Primary key generation interceptor
    /// </summary>
    public class PrimaryKeyInterceptor : ChangeInterceptor<Entity>
    {
        #region Overrides of ChangeInterceptor<Entity>

        /// <summary>
        /// Called when [before insert].
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="item">The item.</param>
        /// <param name="context">The context.</param>
        protected override void OnBeforeInsert(DbEntityEntry entry, Entity item, InterceptionContext context)
        {
            if (item.IsNew)
            {
                item.Id = SequentialGuidGenerator.Generate();
            }

            base.OnBeforeInsert(entry, item, context);
        }

        #endregion
    }
}