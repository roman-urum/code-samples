using System;
using System.Collections.Generic;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// CustomerUser.
    /// </summary>
    public class CustomerUser : User
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the address3.
        /// </summary>
        /// <value>
        /// The address3.
        /// </value>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the national provider identificator.
        /// </summary>
        /// <value>
        /// The national provider identificator.
        /// </value>
        public string NationalProviderIdentificator { get; set; }

        /// <summary>
        /// Gets or sets the customer user identifier.
        /// </summary>
        /// <value>
        /// The customer user identifier.
        /// </value>
        public string CustomerUserId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public Guid? CustomerUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the customer user role.
        /// </summary>
        /// <value>
        /// The customer user role.
        /// </value>
        public virtual CustomerUserRole CustomerUserRole { get; set; }

        /// <summary>
        /// Gets or sets the customer user sites.
        /// </summary>
        /// <value>
        /// The customer user sites.
        /// </value>
        public virtual ICollection<CustomerUserSite> CustomerUserSites { get; set; }
    }
}