using System;

namespace HealthLibrary.Common.Extensions
{
    /// <summary>
    /// Contains extension methods for Guid class.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        /// Identify if value contains empty Guid value.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid target)
        {
            return target == Guid.Empty;
        }

        /// <summary>
        /// Converts Guid value to string without dashes.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string ToStringWithoutDashes(this Guid target)
        {
            return target.ToString("N");
        }
    }
}
