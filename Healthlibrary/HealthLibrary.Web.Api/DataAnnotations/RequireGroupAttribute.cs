using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace HealthLibrary.Web.Api.DataAnnotations
{
    /// <summary>
    /// RequireGroupAttribute.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class RequireGroupAttribute : ValidationAttribute
    {
        private readonly RequireGroupAttributeMode mode;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequireGroupAttribute" /> class.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="mode">The mode.</param>
        public RequireGroupAttribute(string groupName, RequireGroupAttributeMode mode)
        {
            this.GroupName = groupName;
            this.mode = mode;

            switch (mode)
            {
                case RequireGroupAttributeMode.One:
                {
                    ErrorMessage = string.Format("You must select only one value from group \"{0}\"", groupName);

                    break;
                }
                case RequireGroupAttributeMode.OneOrNone:
                {
                    ErrorMessage = string.Format("You must select only one value from group \"{0}\"", groupName);

                    break;
                }
                case RequireGroupAttributeMode.AtLeastOne:
                {
                    ErrorMessage = string.Format("You must select at least one value from group \"{0}\"", groupName);

                    break;
                }
                case RequireGroupAttributeMode.All:
                {
                    ErrorMessage = string.Format("You must select all values from group \"{0}\"", groupName);

                    break;
                }
                case RequireGroupAttributeMode.AllOrNone:
                {
                    ErrorMessage = string.Format("You must select all values from group \"{0}\"", groupName);

                    break;
                }
            }
        }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        /// <value>
        /// The name of the group.
        /// </value>
        public string GroupName { get; private set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var groupProperties = GetGroupProperties(validationContext.ObjectType).ToList();
            var numberOfFilledGroupProperties = groupProperties
                .Count(p => p.GetValue(validationContext.ObjectInstance, null) != null);

            switch (mode)
            {
                case RequireGroupAttributeMode.One:
                {
                    if (numberOfFilledGroupProperties == 1)
                    {
                        return ValidationResult.Success;
                    }

                    break;
                }
                case RequireGroupAttributeMode.OneOrNone:
                {
                    if (numberOfFilledGroupProperties <= 1)
                    {
                        return ValidationResult.Success;
                    }

                    break;
                }
                case RequireGroupAttributeMode.AtLeastOne:
                {
                    if (numberOfFilledGroupProperties > 0)
                    {
                        return ValidationResult.Success;
                    }

                    break;
                }
                case RequireGroupAttributeMode.All:
                {
                    if (numberOfFilledGroupProperties == groupProperties.Count)
                    {
                        return ValidationResult.Success;
                    }

                    break;
                }
                case RequireGroupAttributeMode.AllOrNone:
                {
                    if (numberOfFilledGroupProperties == groupProperties.Count || 
                            numberOfFilledGroupProperties == 0)
                    {
                        return ValidationResult.Success;
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        private IEnumerable<PropertyInfo> GetGroupProperties(Type type)
        {
            return
                from property in type.GetProperties()
                let attributes = property
                    .GetCustomAttributes(typeof(RequireGroupAttribute), false)
                    .OfType<RequireGroupAttribute>()
                where attributes.Any()
                from attribute in attributes
                where attribute.GroupName == GroupName
                select property;
        }
    }

    /// <summary>
    /// RequireGroupAttributeMode enum.
    /// </summary>
    public enum RequireGroupAttributeMode
    {
        /// <summary>
        /// One
        /// </summary>
        One = 1,

        /// <summary>
        /// The one or none
        /// </summary>
        OneOrNone,

        /// <summary>
        /// At least one
        /// </summary>
        AtLeastOne,

        /// <summary>
        /// All
        /// </summary>
        All,

        /// <summary>
        /// All or none
        /// </summary>
        AllOrNone
    }
}