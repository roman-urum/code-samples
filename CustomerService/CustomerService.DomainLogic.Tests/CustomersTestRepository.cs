using System.Linq;
using CustomerService.Domain.Entities;

namespace CustomerService.DomainLogic.Tests
{
    /// <summary>
    /// Test repository to manage customers.
    /// </summary>
    public class CustomersTestRepository : TestRepository<Customer, int>
    {
        /// <summary>
        /// Generates Id for new entity.
        /// </summary>
        /// <returns></returns>
        protected override int GetNextId()
        {
            return this.TestDataSource.Any() ? this.TestDataSource.Max(c => c.Id) + 1 : 1;
        }
    }
}
