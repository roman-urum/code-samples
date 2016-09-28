using System;

namespace Maestro.Domain.Dtos.PatientsService
{
    /// <summary>
    /// IdentifierDto.
    /// </summary>
    public class IdentifierDto
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
        /// Gets or sets a value indicating whether this instance is encrypted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is encrypted; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypted { get; set; }

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
        /// Gets or sets the validation error resource string.
        /// </summary>
        /// <value>
        /// The validation error resource string.
        /// </value>
        public string ValidationErrorResourceString { get; set; }

        /// <summary>
        /// Gets or sets the input mask.
        /// </summary>
        /// <value>
        /// The input mask.
        /// </value>
        public string InputMask { get; set; }
    }
}