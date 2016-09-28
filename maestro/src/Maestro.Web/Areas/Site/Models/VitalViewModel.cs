using System;
using Maestro.Web.Areas.Site.Models.Patients;

namespace Maestro.Web.Areas.Site.Models
{
    /// <summary>
    /// VitalViewModel.
    /// </summary>
    public class VitalViewModel
    {
        /// <summary>
        /// Gets or set the identifier
        /// </summary>
        /// <value>
        /// The identifier
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string Unit { get; set; }

        /// <summary>
        /// Gets or set the alert
        /// </summary>
        /// <value>
        /// the alert
        /// </value>
        public AlertViewModel VitalAlert { get; set; }
    }
}