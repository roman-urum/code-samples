using Maestro.Domain.Dtos.Zoom.Enums;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Description of error in zoom response.
    /// </summary>
    public class ZoomErrorDto
    {
        /// <summary>
        /// Error identifier.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Detailed description of error.
        /// </summary>
        public string Message { get; set; }
    }
}
