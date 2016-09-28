using RestSharp;

namespace DeviceService.ApiAccess.ApiClient
{
    /// <summary>
    /// Model which decribes how request parameter
    /// need to be used.
    /// </summary>
    internal class RequestParameterModel
    {
        /// <summary>
        /// Name of property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Property value as string.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Property type to identify at which place
        /// of request Value should be used.
        /// </summary>
        public ParameterType Type { get; set; }
    }
}
