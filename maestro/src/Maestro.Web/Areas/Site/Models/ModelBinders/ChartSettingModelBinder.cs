using System;
using System.Web.Mvc;
using Maestro.Domain.Enums;
using Maestro.Web.Areas.Site.Models.Patients.Charts;

namespace Maestro.Web.Areas.Site.Models.ModelBinders
{
    /// <summary>
    /// ChartSettingModelBinder.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.DefaultModelBinder" />
    public class ChartSettingModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Creates the specified model type by using the specified controller context and binding context.
        /// </summary>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param>
        /// <param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param>
        /// <param name="modelType">The type of the model object to return.</param>
        /// <returns>
        /// A data object of the specified type.
        /// </returns>
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            string typeString = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".type").AttemptedValue;
            ChartType chartType;

            if (Enum.TryParse(typeString, true, out chartType))
            {
                Type instantiationType = chartType == ChartType.Assessment
                    ? typeof (QuestionChartSettingViewModel)
                    : typeof (VitalChartSettingViewModel);
                var obj = Activator.CreateInstance(instantiationType);

                bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, instantiationType);
                bindingContext.ModelMetadata.Model = obj;

                return obj;
            }

            return base.CreateModel(controllerContext, bindingContext, modelType);
        }
    }
}