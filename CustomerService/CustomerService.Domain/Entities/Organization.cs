using System;
using System.Collections.Generic;

namespace CustomerService.Domain.Entities
{
    /// <summary>
    /// Organization.
    /// </summary>
    public class Organization : Entity<Guid>, ISoftDelitable
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Indicating whether entity deleted or not.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        /// <value>
        /// The customer.
        /// </value>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the parent organization identifier.
        /// </summary>
        /// <value>
        /// The parent organization identifier.
        /// </value>
        public Guid? ParentOrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the parent organization.
        /// </summary>
        /// <value>
        /// The parent organization.
        /// </value>
        public virtual Organization ParentOrganization { get; set; }

        /// <summary>
        /// Gets or sets the child organizations.
        /// </summary>
        /// <value>
        /// The child organizations.
        /// </value>
        public virtual ICollection<Organization> ChildOrganizations { get; set; }

        /// <summary>
        /// Gets or sets the sites.
        /// </summary>
        /// <value>
        /// The sites.
        /// </value>
        public virtual ICollection<Site> Sites { get; set; }
    }
}