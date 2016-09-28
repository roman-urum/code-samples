using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Maestro.Web.Filters
{
    public class ValidateModelStateJson : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;

            if (!modelState.IsValid)
            {

                var errorMessages = new List<string>();

                foreach (KeyValuePair<string, ModelState> keyModelStatePair in modelState)
                {
                    ModelErrorCollection errors = keyModelStatePair.Value.Errors;

                    if (errors != null && errors.Count > 0)
                    {
                        errorMessages.AddRange(
                            errors
                                .Select(error =>
                                {
                                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                                    {
                                        return error.ErrorMessage;
                                    }

                                    if (error.Exception != null && !string.IsNullOrEmpty(error.Exception.Message))
                                    {
                                        return error.Exception.Message;
                                    }

                                    return string.Empty;
                                })
                                .ToArray()
                            );
                    }
                }

                var result = new JsonResult
                {
                    Data = new
                    {
                        ErrorMessage = string.Join(" <> ", errorMessages)
                    }
                };

                filterContext.Result = result;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}