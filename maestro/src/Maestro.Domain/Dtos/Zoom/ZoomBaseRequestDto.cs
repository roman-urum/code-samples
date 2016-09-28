using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.Zoom
{
    /// <summary>
    /// Contains base properties required for requests to Zoom API.
    /// </summary>
    public abstract class ZoomBaseRequestDto
    {
        /// <summary>
        /// API key received from zoom.us
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "api_key")]
        public string ApiKey { get; set; }

        /// <summary>
        /// API secret received from zoom.us
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "api_secret")]
        public string ApiSecret { get; set; }

        /// <summary>
        /// Type of data you are expecting back from the server: "JSON" or "XML"
        /// </summary>
        [RequestParameter(RequestParameterType.RequestBody, "data_type")]
        public string DataType { get; set; }
    }
}
