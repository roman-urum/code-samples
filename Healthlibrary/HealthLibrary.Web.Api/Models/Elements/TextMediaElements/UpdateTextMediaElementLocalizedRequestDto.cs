using System;
using HealthLibrary.Domain.Entities.Enums;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Models.Elements.LocalizedStrings;
using HealthLibrary.Web.Api.Models.Elements.Medias;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.TextMediaElements
{
    /// <summary>
    /// UpdateTextMediaElementLocalizedRequestDto.
    /// </summary>
    public class UpdateTextMediaElementLocalizedRequestDto
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public MediaType? Type { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public UpdateLocalizedStringRequestDto Text { get; set; }

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