using VitalsService.Domain.Enums;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// Model to send data about single vital.
    /// </summary>
    public class VitalDto
    {
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
        [EnumMember(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "EnumMemberAttribute_ValidationError",
            ErrorMessage = null
        )]
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
        [ValidateUnitForVital(
            "Name",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "ValidateUnitForVitalAttribute_ValidationError",
            ErrorMessage = null
        )]
        [EnumMember(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "EnumMemberAttribute_ValidationError",
            ErrorMessage = null
        )]
        public UnitType Unit { get; set; }
    }
}