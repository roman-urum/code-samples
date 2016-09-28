using HealthLibrary.Web.Api.Models.Elements;

namespace HealthLibrary.Web.Api.Models.Protocols
{
    /// <summary>
    /// ProtocolElementResponseDto.
    /// </summary>
    public class ProtocolElementResponseDto : BaseProtocolElementDto
    {
        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        /// <value>
        /// The element.
        /// </value>
        public ElementDto Element { get; set; }
    }
}