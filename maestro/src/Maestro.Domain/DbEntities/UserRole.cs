using System.Collections.Generic;

namespace Maestro.Domain.DbEntities
{
    /// <summary>
    /// UserRole.
    /// </summary>
    public class UserRole : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual ICollection<User> Users { get; set; }
    }
}