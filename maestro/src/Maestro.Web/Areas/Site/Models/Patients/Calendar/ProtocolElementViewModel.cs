using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maestro.Web.Areas.Site.Models.Patients.Calendar
{
    /// <summary>
    /// ProtocolElementViewModel.
    /// </summary>
    public class ProtocolElementViewModel
    {
        /// <summary>
        /// Gets or sets the protocol identifier.
        /// </summary>
        /// <value>
        /// The protocol identifier.
        /// </value>
        public Guid ProtocolId { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        public int Order { get; set; }
    }
}