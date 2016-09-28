using System.Collections.Generic;

namespace CustomerService.Domain.Entities
{
    /// <summary>
    /// Customer.
    /// </summary>
    /// <seealso cref="CustomerService.Domain.ISoftDelitable" />
    public class Customer : Entity<int>, ISoftDelitable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the subdomain.
        /// </summary>
        /// <value>
        /// The subdomain.
        /// </value>
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets the logo path.
        /// </summary>
        /// <value>
        /// The logo path.
        /// </value>
        public string LogoPath { get; set; }
        
        /// <summary>
        /// Gets or sets the password expiration days (days).
        /// </summary>
        /// <value>
        /// The password expiration days.
        /// </value>
        public int PasswordExpirationDays { get; set; }

        /// <summary>
        /// Gets or sets the iddle session timeout (minutes).
        /// </summary>
        /// <value>
        /// The iddle session timeout.
        /// </value>
        public int IddleSessionTimeout { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        public virtual ICollection<Site> Sites { get; set; }

        /// <summary>
        /// Gets or sets the categories of cares.
        /// </summary>
        /// <value>
        /// The categories of cares.
        /// </value>
        public virtual ICollection<CategoryOfCare> CategoriesOfCare { get; set; }

        /// <summary>
        /// Gets or sets the organizations.
        /// </summary>
        /// <value>
        /// The organizations.
        /// </value>
        public virtual ICollection<Organization> Organizations { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
