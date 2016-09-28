using System.ComponentModel;

namespace VitalsService.Domain.Enums
{
    /// <summary>
    /// UnitType.
    /// </summary>
    public enum UnitType
    {
        /// <summary>
        /// The millimeter of mercury.
        /// </summary>
        mmHg = 1,

        /// <summary>
        /// The killo pascal.
        /// </summary>
        kPa,

        /// <summary>
        /// The beats per minute.
        /// </summary>
        BpM,

        /// <summary>
        /// The percent.
        /// </summary>
        Percent,

        /// <summary>
        /// The pound.
        /// </summary>
        Lbs,

        /// <summary>
        /// The kilogram.
        /// </summary>
        kg,

        /// <summary>
        /// The celsius.
        /// </summary>
        [Description("C")]
        C,

        /// <summary>
        /// The fahrenheit.
        /// </summary>
        F,

        /// <summary>
        /// The milligrams per deciliter.
        /// </summary>
        mgdl,

        /// <summary>
        /// The millimoles per liter.
        /// </summary>
        mmol_L,

        /// <summary>
        /// The kilogram force per square meter.
        /// </summary>
        kg_m2,

        /// <summary>
        /// The liter per minute.
        /// </summary>
        L_min,

        /// <summary>
        /// The liter.
        /// </summary>
        L,

        /// <summary>
        /// Amount of steps per day.
        /// </summary>
        steps_day
    }
}