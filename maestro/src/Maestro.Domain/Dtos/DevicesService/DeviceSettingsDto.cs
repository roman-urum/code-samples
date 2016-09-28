using Maestro.Domain.Dtos.DevicesService.Enums;

namespace Maestro.Domain.Dtos.DevicesService
{
    /// <summary>
    /// DeviceSettingsDto.
    /// </summary>
    public class DeviceSettingsDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is weight automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is weight automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsWeightAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is weight manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is weight manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsWeightManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is blood pressure automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blood pressure automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsBloodPressureAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is blood pressure manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blood pressure manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsBloodPressureManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pulse ox automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pulse ox automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPulseOxAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pulse ox manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pulse ox manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsPulseOxManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is blood glucose automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blood glucose automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsBloodGlucoseAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is blood glucose manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is blood glucose manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsBloodGlucoseManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is peak flow automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is peak flow automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPeakFlowAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is peak flow manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is peak flow manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsPeakFlowManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is temperature automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is temperature automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsTemperatureAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is temperature manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is temperature manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsTemperatureManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is temperature automated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is temperature automated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPedometerAutomated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pedometer manual.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pedometer manual; otherwise, <c>false</c>.
        /// </value>
        public bool IsPedometerManual { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is pin code required.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is pin code required; otherwise, <c>false</c>.
        /// </value>
        public bool IsPinCodeRequired { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the blood glucose peripheral.
        /// </summary>
        public BloodGlucosePeripheral? BloodGlucosePeripheral { get; set; }
    }
}