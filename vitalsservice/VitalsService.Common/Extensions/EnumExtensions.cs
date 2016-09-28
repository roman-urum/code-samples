using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace VitalsService.Extensions
{
    /// <summary>
    /// EnumExtensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Descriptions the specified enum value.
        /// </summary>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public static String Description(this Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            FieldInfo field = enumType.GetField(enumValue.ToString());
            Object[] attributes =
                field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
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

        /// <summary>
        /// Gets the flags.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            return Enum.GetValues(input.GetType()).Cast<Enum>().Where(input.HasFlag);
        }
    }
}