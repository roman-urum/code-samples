using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// HealthSessionElementDto.
    /// </summary>
    public class BaseHealthSessionElementDto
    {
        /// <summary>
        /// Guid of element from health library (text, question, etc).
        /// </summary>
        [ForbidDefaultNotNullableValue(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "ForbidDefaultNotNullableValueAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid ElementId { get; set; }

        /// <summary>
        /// Value or the element (question, text, etc)
        /// </summary>
        [MaxLength(
            DbConstraints.MaxLength.HealthSessionElementText,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Text { get; set; }

        /// <summary>
        /// Type of element.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public HealthSessionElementType Type { get; set; }

        /// <summary>
        /// Gets or sets the media type.
        /// </summary>
        public MediaType? MediaType { get; set; }

        /// <summary>
        /// Gets or sets the media identifier.
        /// </summary>
        public Guid? MediaId { get; set; }

        /// <summary>
        /// Gets or set the media name.
        /// </summary>
        public string MediaName { get; set; }

        /// <summary>
        /// Gets or sets the internal question Id.
        /// </summary>
        [MaxLength(
            DbConstraints.MaxLength.InternalId,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [NotEmptyString(
            ErrorMessageResourceName = "NotEmptyStringAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessage = null
        )]
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external question Id.
        /// </summary>
        [MaxLength(
            DbConstraints.MaxLength.ExternalId,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [NotEmptyString(
            ErrorMessage = null,
            ErrorMessageResourceName = "NotEmptyStringAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings)
        )]
        public string ExternalId { get; set; }

        /// <summary>
        /// Answer for element.
        /// </summary>
        // Note: Commented according to https://careinnovations.atlassian.net/browse/MS-1375
        //[ItemsRequiredByElementType(
        //    "Type", 
        //    HealthSessionElementType.Measurement,
        //    HealthSessionElementType.Question
        //)]
        [ElementValueTypeValidation("Type")]
        public IList<HealthSessionElementValueDto> Values { get; set; }
    }
}