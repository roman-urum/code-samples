using System.Collections.Generic;

namespace Maestro.Common.Extensions
{
    /// <summary>
    /// Extension methods for instances of bool class.
    /// </summary>
    public static class BoolExtensions
    {
        private static readonly IDictionary<bool, string> StatesMap = new Dictionary<bool, string>
        {
            {true, "ON"},
            {false, "OFF"}
        };

        /// <summary>
        /// Converts boolean value to string which identifies state (ON/OFF)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToStateString(this bool target)
        {
            return StatesMap[target];
        }
    }
}
