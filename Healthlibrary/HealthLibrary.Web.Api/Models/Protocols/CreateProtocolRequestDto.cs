using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// CreateProtocolRequestDto.
    /// </summary>
    public class CreateProtocolRequestDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null,
            AllowEmptyStrings = false
        )]
        [StringLength(
            50,
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is private.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is private; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivate { get; set; }

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
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "TagsValidationMessage",
            ErrorMessage = null
        )]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the first protocol element identifier.
        /// </summary>
        /// <value>
        /// The first protocol element identifier.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid FirstProtocolElementId { get; set; }

        /// <summary>
        /// Gets or sets the protocol elements.
        /// </summary>
        /// <value>
        /// The protocol elements.
        /// </value>
        [NotEmptyList(
            ErrorMessageResourceType = typeof (GlobalStrings),
            ErrorMessageResourceName = "NotEmptyListAttribute_ValidationError",
            ErrorMessage = null
        )]
        public IList<ProtocolElementRequestDto> ProtocolElements { get; set; }
    }
}