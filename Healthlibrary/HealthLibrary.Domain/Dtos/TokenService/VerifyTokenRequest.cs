using HealthLibrary.Common.ApiClient;

namespace HealthLibrary.Domain.Dtos.TokenService
{
    /// <summary>
    /// Data of request to verify token access permissions.
    /// </summary>
    public class VerifyTokenRequest
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [RequestParameter(RequestParameterType.UrlSegment)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public string Controller { get; set; }

        /// <summary>
        /// Name of service to verify.
        /// </summary>
        [RequestParameter(RequestParameterType.QueryString)]
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the customer number.
        /// </summary>
        /// <value>
        /// The customer number.
        /// </value>
        [RequestParameter(RequestParameterType.QueryString)]
        public int? Customer { get; set; }
    }
}
