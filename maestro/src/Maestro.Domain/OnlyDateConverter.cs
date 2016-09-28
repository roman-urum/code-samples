using Newtonsoft.Json.Converters;

namespace Maestro.Domain
{
    /// <summary>
    /// OnlyDateConverter.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Converters.IsoDateTimeConverter" />
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