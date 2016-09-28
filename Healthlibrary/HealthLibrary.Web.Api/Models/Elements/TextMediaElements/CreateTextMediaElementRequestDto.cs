using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.TextMediaElements
{
    /// <summary>
    /// CreateTextMediaElementDto.
    /// </summary>
    public class CreateTextMediaElementRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null,
            AllowEmptyStrings = false
        )]
        [StringLength(
            50,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "TextMediaElementsName_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MediaType? Type { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [ElementStringLength(
            30,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [UniqueStringsList(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "UniqueStringsListAttribute_ValidationError",
            ErrorMessage = null
        )]
        [ListRegularExpression(
            ValidationExpressions.Tag,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "TagsValidationMessage",
            ErrorMessage = null
        )]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public CreateLocalizedStringRequestDto Text { get; set; }

        /// <summary>
        /// Id of existing media which already was uploaded.
        /// </summary>
        [RequireGroup(
            "Media",
            RequireGroupAttributeMode.OneOrNone,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MediaRequireGroup_ValidationError",
            ErrorMessage = null
        )]
        public Guid? MediaId { get; set; }

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        [RequireGroup(
            "Media",
            RequireGroupAttributeMode.OneOrNone,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MediaRequireGroup_ValidationError",
            ErrorMessage = null
        )]
        public UpdateMediaRequestDto Media { get; set; }
    }
}