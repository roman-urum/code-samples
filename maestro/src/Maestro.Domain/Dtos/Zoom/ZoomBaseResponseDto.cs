namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Contains default properties for all responses from Zoom API.
    /// </summary>
    public abstract class ZoomBaseResponseDto
    {
        /// <summary>
        /// Info about error occured during handling of request.
        /// Null if response has no errors.
        /// </summary>
        public ZoomErrorDto Error { get; set; }
    }
}
