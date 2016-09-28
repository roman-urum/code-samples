using System;

namespace CustomerService.Common.Exceptions
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Includes values in template string.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string target, params object[] args)
        {
            if (string.IsNullOrEmpty(target))
            {
                throw new ArgumentNullException("target");
            }

            return string.Format(target, args);
        }
    }
}
