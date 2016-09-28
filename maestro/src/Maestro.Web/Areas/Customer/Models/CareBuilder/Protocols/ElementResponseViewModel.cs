using System;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    /// <summary>
    /// ElementResponseViewModel.
    /// </summary>
    public class ElementResponseViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        /// The customer identifier.
        /// </value>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ElementType Type { get; set; }
    }
}