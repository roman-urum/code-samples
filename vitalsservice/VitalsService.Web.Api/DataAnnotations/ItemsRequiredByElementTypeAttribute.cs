using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using VitalsService.Domain.Enums;
using VitalsService.Web.Api.Extensions;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Attribute determines if field is required in depending on
    /// value of health element type.
    /// </summary>
    public class ItemsRequiredByElementTypeAttribute : ValidationAttribute
    {
        private readonly string typeFieldName;
        private readonly List<HealthSessionElementType> elementTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemsRequiredByElementTypeAttribute"/> class.
        /// </summary>
        /// <param name="typeFieldName">Name of the type field.</param>
        /// <param name="elementTypes">The element types.</param>
        /// <exception cref="System.ArgumentNullException">
        /// typeFieldName
        /// or
        /// elementTypes
        /// </exception>
        public ItemsRequiredByElementTypeAttribute(string typeFieldName, params HealthSessionElementType[] elementTypes)
        {
            if (string.IsNullOrEmpty(typeFieldName))
            {
                throw new ArgumentNullException("typeFieldName");
            }

            if (!elementTypes.Any())
            {
                throw new ArgumentNullException("elementTypes");
            }

            this.typeFieldName = typeFieldName;
            this.elementTypes = elementTypes.ToList();
        }

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var typeValue = validationContext.ReadPropertyValue(typeFieldName);
            HealthSessionElementType actualType;

            if (typeValue == null || !Enum.TryParse(typeValue.ToString(), out actualType))
            {
                return ValidationResult.Success;
            }

            var list = value as IList;

            if (!elementTypes.Contains(actualType) || list != null && list.Count > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(GlobalStrings.RequiredByElementTypeAttribute_ValidationError);
        }
    }
}