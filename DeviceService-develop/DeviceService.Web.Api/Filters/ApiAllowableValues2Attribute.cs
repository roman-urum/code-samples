using System;
using System.Collections.Generic;
using System.Linq;

namespace DeviceService.Web.Api.Filters
{
    /// <summary>
    /// ApiAllowableValues2Attribute.
    /// </summary>
    public class ApiAllowableValues2Attribute : ApiAllowableValuesAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiAllowableValues2Attribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="enumType">Type of the enum.</param>
        public ApiAllowableValues2Attribute(string name, Type enumType)
            : base(name)
        {
            var values = new List<string>();

            var enumTypeValues = Enum.GetValues(enumType);

            // loop through each enum value
            foreach (var etValue in enumTypeValues)
            {
                // get the member in order to get the enumMemberAttribute
                var member = enumType.GetMember(
                    Enum.GetName(enumType, etValue)).First();

                // get the enumMember attribute
                var enumMemberAttr = member.GetCustomAttributes(
                    typeof(System.Runtime.Serialization.EnumMemberAttribute), true).First();

                // get the enumMember attribute value
                var enumMemberValue = ((System.Runtime.Serialization.EnumMemberAttribute)enumMemberAttr).Value;

                values.Add(enumMemberValue);

            }

            Values = values.ToArray();
        }
    }

}