using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.Medias
{
    /// <summary>
    /// BaseMediaRequestDto.
    /// </summary>
    public abstract class BaseMediaRequestDto
    {
        public const int MaxMediaFileSize = 157286400;

        /// <summary>
        /// Name of file to create.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }

        /// <summary>
        /// Description of media file.
        /// </summary>
        [StringLength(
            1000,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [ElementStringLength(
            30,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [UniqueStringsList(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "UniqueStringsListAttribute_ValidationError",
            ErrorMessage = null
            )]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// File content in base-64 string.
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// Gets or sets the source content URL.
        /// </summary>
        /// <value>
        /// The source content URL.
        /// </value>
        public virtual string SourceContentUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the original file.
        /// </summary>
        /// <value>
        /// The name of the original file.
        /// </value>
        public virtual string OriginalFileName { get; set; }

        /// <summary>
        /// Type of file content.
        /// </summary>
        public virtual string ContentType { get; set; }
    }
}