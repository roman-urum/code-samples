using System;

namespace Maestro.Web.Areas.Site.Models.Patients
{
    /// <summary>
    /// IdentifierViewModel.
    /// </summary>
    public class IdentifierViewModel
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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is required; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is primary; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Gets or sets the validation reg ex.
        /// </summary>
        /// <value>
        /// The validation reg ex.
        /// </value>
        public string ValidationRegEx { get; set; }

        /// <summary>
        /// Gets or sets the validation error message.
        /// </summary>
        /// <value>
        /// The validation error message.
        /// </value>
        public string ValidationErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the input mask.
        /// </summary>
        /// <value>
        /// The input mask.
        /// </value>
        public string InputMask { get; set; }
    }
}