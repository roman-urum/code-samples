using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Protocols;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    /// <summary>
    /// ProtocolElementResponseViewModel.
    /// </summary>
    public class ProtocolElementResponseViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
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
        public List<BranchDto> Branches { get; set; }

        /// <summary>
        /// Gets or sets the alerts.
        /// </summary>
        /// <value>
        /// The alerts.
        /// </value>
        public List<AlertDto> Alerts { get; set; }

        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public ElementResponseViewModel Element { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ended.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ended; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnded { get; set; }
    }
}