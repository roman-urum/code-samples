using System;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// ProtocolElementRequestDto.
    /// </summary>
    public class ProtocolElementRequestDto : BaseProtocolElementDto
    {
        /// <summary>
        /// Gets or sets the element identifier.
        /// </summary>
        /// <value>
        /// The element identifier.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid ElementId { get; set; }
    }
}