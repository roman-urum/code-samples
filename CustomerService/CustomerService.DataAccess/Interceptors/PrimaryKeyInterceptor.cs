using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerService.Domain;

namespace CustomerService.DataAccess.Interceptors
{
    using System.Data.Entity.Infrastructure;

    using CustomerService.Common.Helpers;
    using CustomerService.Domain.Entities;

    using Isg.EntityFramework.Interceptors;

    public class PrimaryKeyInterceptor: ChangeInterceptor<Entity<Guid>>
    {
        protected override void OnBeforeInsert(DbEntityEntry entry, Entity<Guid> item, InterceptionContext context)
        {
            if (item.IsNew)
            {
                item.Id = SequentialGuidGenerator.Generate();
            }

            base.OnBeforeInsert(entry, item, context);
        }
    }
}
