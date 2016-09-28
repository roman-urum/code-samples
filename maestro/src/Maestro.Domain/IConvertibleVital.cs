using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain
{
    /// <summary>
    /// IConvertibleVital.
    /// </summary>
    public interface IConvertibleVital
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        VitalType Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        UnitType Unit { get; set; }
    }
}