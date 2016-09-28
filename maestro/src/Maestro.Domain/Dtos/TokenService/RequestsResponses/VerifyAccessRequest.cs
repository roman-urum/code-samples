using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.TokenService.RequestsResponses
{
    /// <summary>
    /// VerifyAccessRequest
    /// </summary>
    public class VerifyAccessRequest
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
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Action { get; set; }
        /// <summary>
        /// Gets or sets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        [RequestParameter(RequestParameterType.RequestBody)]
        public string Controller { get; set; }
        /// <summary>
        /// Gets or sets the customer number.
        /// </summary>
        /// <value>
        /// The customer number.
        /// </value>
        [RequestParameter(RequestParameterType.RequestBody)]
        public int? Customer { get; set; }
    }
}