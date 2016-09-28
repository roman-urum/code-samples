using System.Linq;
using System.Web.Http.Description;
using Swashbuckle.Swagger;
using VitalsService.Web.Api.DataAnnotations;

namespace VitalsService.Web.Api.SwaggerExtensions.Filters
{
    public class AddFileUploadFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var fileAttributes = apiDescription.GetControllerAndActionAttributes<SwaggerAddFileUploadAttribute>();

            foreach (var fileUploadAttribute in fileAttributes)
            {
                operation.consumes.Add("multipart/form-data");

                operation.parameters.Add(new Parameter
                {
                    name = fileUploadAttribute.ParameterName,
                    @in = "formData",
                    description = fileUploadAttribute.Description,
                    required = fileUploadAttribute.Required,
                    type = "file"
                });
            }
        }
    }
}