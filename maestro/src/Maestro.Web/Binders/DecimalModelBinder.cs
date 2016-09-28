using System;
using System.Globalization;
using System.Web.Mvc;

namespace Maestro.Web.Binders
{
    /// <summary>
    /// DecimalModelBinder.
    /// </summary>
    public class DecimalModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns></returns>
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext
        )
        {
            ValueProviderResult valueResult = 
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            try
            {
                actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                modelState.Errors.Add(ex);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}