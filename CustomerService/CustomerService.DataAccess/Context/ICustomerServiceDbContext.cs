using System.Data.Entity;
using CustomerService.Domain.Entities;

namespace CustomerService.DataAccess.Context
{
    /// <summary>
    /// ICustomerServiceDbContext.
    /// </summary>
    public interface ICustomerServiceDbContext
    {
        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        DbSet<Site> Sites { get; set; }
    }
}