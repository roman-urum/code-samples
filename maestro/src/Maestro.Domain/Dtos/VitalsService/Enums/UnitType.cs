using System.ComponentModel;

namespace Maestro.Domain.Dtos.VitalsService.Enums
{
    /// <summary>
    /// UnitType.
    /// </summary>
    public enum UnitType
    {
        /// <summary>
        /// The millimeter of mercury.
        /// </summary>
        [Description("mmHg")]
        mmHg = 1,

        /// <summary>
        /// The killo pascal.
        /// </summary>
        [Description("kPa")]
        kPa,

        /// <summary>
        /// The beats per minute.
        /// </summary>
        [Description("BpM")]
        BpM,

        /// <summary>
        /// The percent.
        /// </summary>
        [Description("%")]
        Percent,

        /// <summary>
        /// The pound.
        /// </summary>
        [Description("Lbs")]
        Lbs,

        /// <summary>
        /// The kilogram.
        /// </summary>
        [Description("kg")]
        kg,

        /// <summary>
        /// The celsius.
        /// </summary>
        [Description("C")]
        C,

        /// <summary>
        /// The fahrenheit.
        /// </summary>
        [Description("F")]
        F,

        /// <summary>
        /// The milligrams per deciliter.
        /// </summary>
        [Description("mg/dL")]
        mgdl,

        /// <summary>
        /// The millimoles per liter.
        /// </summary>
        [Description("mmol/L")]
        mmol_L,

        /// <summary>
        /// The kilogram force per square meter.
        /// </summary>
        [Description("kg/m2")]
        kg_m2,

        /// <summary>
        /// The liter per minute.
        /// </summary>
        [Description("L/min")]
        L_min,

        /// <summary>
        /// The liter.
        /// </summary>
        [Description("L")]
        L,

        /// <summary>
        /// Amount of steps per day.
        /// </summary>
        [Description("steps/day")]
        steps_day
    }
}