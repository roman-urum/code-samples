using System;

namespace VitalsService.Web.Api.DataAnnotations
{
    /// <summary>
    /// Required to add possibility to upload files in swagger.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerAddFileUploadAttribute : Attribute
    {
        public bool Required { get; private set; }

        public string Description { get; private set; }

        public string ParameterName { get; private set; }

        public SwaggerAddFileUploadAttribute(string parameterName, string description = null, bool required = true)
        {
            this.ParameterName = parameterName;
            this.Description = description;
            this.Required = required;
        }
    }
}