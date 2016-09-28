using System;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Domain.Dtos.VitalsService
{
    /// <summary>
    /// VitalDto.
    /// </summary>
    public class VitalDto : IConvertibleVital
    {
        /// <summary>
        /// Gets or set the identifier
        /// </summary>
        /// <value>
        /// The identifier
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the indicator whether the vital is automated or not.
        /// </summary>
        /// <value>
        /// The indicator whether the vital is automated or not.
        /// </value>
        public bool IsAutomated { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public VitalType Name { get; set; }

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
        public UnitType Unit { get; set; }
    }
}