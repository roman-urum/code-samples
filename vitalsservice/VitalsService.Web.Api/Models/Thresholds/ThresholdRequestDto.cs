using System;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.Thresholds
{
    /// <summary>
    /// ThresholdRequestDto.
    /// </summary>
    public class ThresholdRequestDto
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [EnumMember(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "EnumMemberAttribute_ValidationError",
            ErrorMessage = null
        )]
        public ThresholdType Type { get; set; }

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
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        [Range(
            0,
            double.MaxValue,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanZero_ValidationError",
            ErrorMessage = null
        )]
        public decimal MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        [Range(
            0,
            double.MaxValue,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanZero_ValidationError",
            ErrorMessage = null
        )]
        [GreaterThan(
            "MinValue",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanAttribute_ValidationError",
            ErrorMessage = null)]
        [ValidateMaximumValueForVital(
            "Name",
            "Unit",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "ValidateMaximumValueForVitalAttribute_ValidationError",
            ErrorMessage = null
        )]
        public decimal MaxValue { get; set; }

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

        /// <summary>
        /// Gets or sets the alert severity.
        /// </summary>
        /// <value>
        /// The alert severity.
        /// </value>
        public Guid? AlertSeverityId { get; set; }
    }
}