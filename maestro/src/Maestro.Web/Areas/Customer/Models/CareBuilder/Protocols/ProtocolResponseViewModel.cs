using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.Protocols
{
    /// <summary>
    /// ProtocolResponseDto.
    /// </summary>
    public class ProtocolResponseViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public LocalizedStringResponseDto Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the first protocol element identifier.
        /// </summary>
        /// <value>
        /// The first protocol element identifier.
        /// </value>
        public Guid FirstProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the protocol elements.
        /// </summary>
        /// <value>
        /// The protocol elements.
        /// </value>
        public IList<ProtocolElementResponseViewModel> ProtocolElements { get; set; }
    }
}