using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.Medias
{
    /// <summary>
    /// Dto to update data of existed media.
    /// </summary>
    public class UpdateMediaRequestDto : BaseMediaRequestDto
    {
        /// <summary>
        /// File content in base-64 string.
        /// </summary>
        [Base64(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "InvalidBase64String_ValidationError",
            ErrorMessage = null
            )]
        [NotRequiredIfNotEmpty(
            "SourceContentUrl",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "NotRequiredIfNotEmptyAttribute_ValidationError",
            ErrorMessage = null)]
        [Base64FileSize(MaxMediaFileSize,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "Base64FileSizeAttribute_ValidationError",
            ErrorMessage = null)]
        public override string Content { get; set; }

        /// <summary>
        /// Gets or sets the source content URL.
        /// </summary>
        /// <value>
        /// The source content URL.
        /// </value>
        [MediaUrl(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "InvalidUrlString_ValidationError",
            ErrorMessage = null
            )]
        [NotRequiredIfNotEmpty(
            "Content",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "NotRequiredIfNotEmptyAttribute_ValidationError",
            ErrorMessage = null)]
        public override string SourceContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>
        /// The name of the original file.
        /// </value>
        [RequiredIfEmpty("SourceContentUrl")]
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [FileName(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "FileNameAttribute_ValidationError",
            ErrorMessage = null
        )]
        public override string OriginalFileName { get; set; }

        /// <summary>
        /// Type of file content.
        /// </summary>
        [RequiredIfEmpty(
            "SourceContentUrl",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredIfEmptyAttribute_ValidationError",
            ErrorMessage = null)]
        [StringLength(50,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "Media_ContentTypeIsTooLongError",
            ErrorMessage = null
            )]
        [SupportedContentType(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "SupportedContentTypeAttribute_ValidationError",
            ErrorMessage = null)]
        public override string ContentType { get; set; }
    }
}