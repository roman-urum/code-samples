namespace Maestro.Domain.Dtos.PatientsService
{
    /// <summary>
    /// PatientIdentifierDto.
    /// </summary>
    public class PatientIdentifierDto
    {
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
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets IsRequired flag
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the IsPrimary flag.
        /// </summary>
        /// <value>
        /// The IsPrimary flag.
        /// </value>
        public bool IsPrimary { get; set; }
    }
}