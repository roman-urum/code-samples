using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using CustomerService.Common.Extensions;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Enums;

namespace CustomerService.Web.Api.Filters
{
    /// <summary>
    /// Validates model state and returns <see cref="HttpStatusCode.BadRequest"/>
    /// with error description unless it is valid.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs before the action method is invoked.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            foreach (var actionArgument in actionContext.ActionArguments.Where(a => a.Value == null))
            {
                var actionArgumentType = actionContext
                    .ActionDescriptor
                    .GetParameters()
                    .Single(x => x.ParameterName == actionArgument.Key)
                    .ParameterType;

                if (!(actionArgumentType.IsGenericType &&
                      actionArgumentType.GetGenericTypeDefinition() == typeof(Nullable<>) ||
                      actionArgumentType == typeof(string)))
                {
                    actionContext.Response =
                    actionContext.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidRequest,
                            Message = ErrorCode.InvalidRequest.Description(),
                            Details = this.GenerateErrorMessageDetails(modelState)
                        });

                    return;
                }
            }

            if (!modelState.IsValid)
            {
                actionContext.Response =
                    actionContext.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new ErrorResponseDto()
                        {
                            Error = ErrorCode.InvalidRequest,
                            Message = ErrorCode.InvalidRequest.Description(),
                            Details = GenerateErrorMessageDetails(modelState)
                        });
            }
        }

        /// <summary>
        /// Generates the error message details.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <returns></returns>
        private string GenerateErrorMessageDetails(ModelStateDictionary modelState)
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

            return string.Join(" <> ", errorMessages);
        }
    }
}