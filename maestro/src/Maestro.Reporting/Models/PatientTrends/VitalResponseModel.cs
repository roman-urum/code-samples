using System;
using Maestro.Domain.Dtos.VitalsService.Enums;

namespace Maestro.Reporting.Models.PatientTrends
{
    /// <summary>
    /// VitalResponseModel.
    /// </summary>
    public class VitalResponseModel
    {
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

        /// <summary>
        /// Gets or sets the observed.
        /// </summary>
        /// <value>
        /// The observed.
        /// </value>
        public DateTimeOffset Observed { get; set; }
    }
}