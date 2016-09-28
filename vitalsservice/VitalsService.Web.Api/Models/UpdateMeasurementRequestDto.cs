namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// UpdateMeasurementRequestDto.
    /// </summary>
    public class UpdateMeasurementRequestDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UpdateMeasurementRequestDto"/> is invalidated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if invalidated; otherwise, <c>false</c>.
        /// </value>
        public bool IsInvalidated { get; set; }
    }
}