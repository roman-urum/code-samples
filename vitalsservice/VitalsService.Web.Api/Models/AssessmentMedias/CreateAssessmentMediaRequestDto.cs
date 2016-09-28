using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models.AssessmentMedias
{
    /// <summary>
    /// Model for data of request to create new assessment media.
    /// </summary>
    public class CreateAssessmentMediaRequestDto
    {
        /// <summary>
        /// Original name of media file.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [MaxLength(DbConstraints.MaxLength.FileName,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string OriginalFileName { get; set; }

        /// <summary>
        /// Type of file content.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [MaxLength(DbConstraints.MaxLength.ContentType,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string ContentType { get; set; }

        /// <summary>
        /// Size of file content in bytes.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [Range(1, 157286400,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RangeAttribute_ValidationError",
            ErrorMessage = null
        )]
        public long ContentLength { get; set; }

        /// <summary>
        /// Type of uploaded meadia.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public MediaType MediaType { get; set; }
    }
}