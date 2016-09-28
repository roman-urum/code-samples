using System;
using CustomerService.Domain;

namespace CustomerService.DomainLogic.Tests
{
    public class DefaultTestRepository<T> : TestRepository<T, Guid> where T : Entity<Guid>, new()
    {
        /// <summary>
        /// Generates Id for new entity.
        /// </summary>
        /// <returns></returns>
        protected override Guid GetNextId()
        {
            return Guid.NewGuid();
        }
    }
}
