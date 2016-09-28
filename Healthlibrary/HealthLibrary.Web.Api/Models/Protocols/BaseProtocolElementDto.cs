using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// BaseProtocolElementDto.
    /// </summary>
    public abstract class BaseProtocolElementDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sort.
        /// </summary>
        /// <value>
        /// The sort.
        /// </value>
        public int Sort { get; set; }

        /// <summary>
        /// Gets or sets the next protocol element identifier.
        /// </summary>
        /// <value>
        /// The next protocol element identifier.
        /// </value>
        public Guid? NextProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the branches.
        /// </summary>
        /// <value>
        /// The branches.
        /// </value>
        public IList<BranchDto> Branches { get; set; }

        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>
        /// The alerts.
        /// </value>
        public IList<AlertDto> Alerts { get; set; }
    }
}