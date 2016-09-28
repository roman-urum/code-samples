using System;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.Thresholds
{
    /// <summary>
    /// DefaultThresholdRequestDto.
    /// </summary>
    public class DefaultThresholdRequestDto : ThresholdRequestDto
    {
        /// <summary>
        /// Gets or sets the default type.
        /// </summary>
        /// <value>
        /// The default type.
        /// </value>
        [EnumMember(
            ErrorMessageResourceType = typeof(GlobalStrings), 
            ErrorMessageResourceName = "EnumMemberAttribute_ValidationError", 
            ErrorMessage = null
        )]        
        public ThresholdDefaultType DefaultType { get; set; }

        /// <summary>
        /// Gets or sets the condition identifier.
        /// </summary>
        /// <value>
        /// The condition identifier.
        /// </value>
        [ConditionIdValidation(
            "DefaultType",
            ErrorMessage = null,
            ErrorMessageResourceName = "ConditionIdValidation_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings))]

        public Guid? ConditionId { get; set; }
    }
}