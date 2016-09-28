using Newtonsoft.Json.Converters;

namespace DeviceService.Web.Api.Helpers
{
    /// <summary>
    /// OnlyDateConverter
    /// </summary>
    public class OnlyDateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OnlyDateConverter"/> class.
        /// </summary>
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}