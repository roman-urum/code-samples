using System;

namespace DeviceService.Common.Extensions
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

        /// <summary>
        /// Determines whether the specified string contains any element of provided list of words.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="words">The words.</param>
        /// <returns></returns>
        public static bool ContainsAny(this string target, params string[] words)
        {
            foreach (string word in words)
            {
                if (target.Contains(word))
                {
                    return true;
                }
            }

            return false;
        }
    }
}