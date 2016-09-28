using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Maestro.Common.Extensions
{
    /// <summary>
    /// Extension methods for enums members.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            Type enumType = value.GetType();
            FieldInfo field = enumType.GetField(value.ToString());
            object[] attributes =
                field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return !attributes.Any()
                ? value.ToString()
                : ((DescriptionAttribute)attributes.First()).Description;
        }

        /// <summary>
        /// Determines whether the specified flag contains flag.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="flag">The flag.</param>
        /// <returns></returns>
        public static bool ContainsFlag(this Enum source, Enum flag)
        {
            var sourceValue = ToUInt64(source);
            var flagValue = ToUInt64(flag);

            return (sourceValue & flagValue) == flagValue;
        }

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            return Enum.GetValues(input.GetType()).Cast<Enum>().Where(input.HasFlag);
        }

        /// <summary>
        /// Gets the concat string.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string GetConcatString(this Enum target)
        {
            return string.Join(" <> ", target.GetFlags().Select(f => f.Description()));
        }

        private static ulong ToUInt64(object value)
        {
            switch (Convert.GetTypeCode(value))
            {
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);

                case TypeCode.Byte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return Convert.ToUInt64(value, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException("Unknown enum type.");
        }
    }
}