using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Web.Api.DataAnnotations;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.HealthSessions
{
    /// <summary>
    /// Model for free form answer.
    /// </summary>
    public class FreeFormAnswerDto : HealthSessionElementValueDto
    {
        /// <summary>
        /// A free form answer.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the internal question id.
        /// </summary>
        [MaxLength(
            DbConstraints.MaxLength.InternalId,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [NotEmptyString(
            ErrorMessage = null,
            ErrorMessageResourceName = "NotEmptyStringAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings)
        )]
        public string InternalId { get; set; }

        /// <summary>
        /// Gets or sets the external questino id.
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
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? ExternalScore { get; set; }

        /// <summary>
        /// Gets or sets the External score.
        /// </summary>
        /// <value>
        /// Int.
        /// </value>
        public int? InternalScore { get; set; }
    }
}