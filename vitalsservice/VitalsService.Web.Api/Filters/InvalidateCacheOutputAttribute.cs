using System;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebApi.OutputCache.V2;

namespace VitalsService.Web.Api.Filters
{
    /// <summary>
    /// InvalidateCacheOutputAttribute (Note: This is FIXED version of WebApi.OutputCache.V2 InvalidateCacheOutputAttribute).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class InvalidateCacheOutputAttribute : BaseCacheAttribute
    {
        private string controller;
        private readonly string methodName;
        private readonly string[] actionParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidateCacheOutputAttribute"/> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        public InvalidateCacheOutputAttribute(string methodName)
          : this(methodName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidateCacheOutputAttribute" /> class.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <param name="actionParameters">The action parameters.</param>
        public InvalidateCacheOutputAttribute(
            string methodName,
            Type controllerType,
            params string[] actionParameters
        )
        {
            this.controller = controllerType != (Type)null ? controllerType.FullName : (string)null;
            this.methodName = methodName;
            this.actionParameters = actionParameters;
        }

        /// <summary>
        /// Called when [action executed].
        /// </summary>
        /// <param name="actionExecutedContext">The action executed context.</param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
#if !DEBUG
            if (actionExecutedContext.Response != null && !actionExecutedContext.Response.IsSuccessStatusCode)
            {
                return;
            }

            this.controller = this.controller ?? actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName;

            this.EnsureCache(actionExecutedContext.Request.GetConfiguration(), actionExecutedContext.Request);

            string baseCachekey = actionExecutedContext.Request.GetConfiguration().CacheOutputConfiguration().MakeBaseCachekey(this.controller, this.methodName);

            string key = IncludeActionParameters(actionExecutedContext.ActionContext, baseCachekey, actionParameters);

            if (!this.WebApiCache.Contains(key))
            {
                return;
            }

            this.WebApiCache.RemoveStartsWith(key);
#endif
        }

        private string IncludeActionParameters(HttpActionContext actionContext, string baseCachekey, string[] additionalActionParameters)
        {
            if (!additionalActionParameters.Any())
            {
                return string.Format("{0}", baseCachekey);
            }

            var actionContextParameters = actionContext.ActionArguments
                .Where(x => x.Value != null && additionalActionParameters.Contains(x.Key))
                .Select(x => x.Key + "=" + GetValue(x.Value));

            return string.Format("{0}-*{1}", baseCachekey, string.Join("*", actionContextParameters));
        }

        private string GetValue(object val)
        {
            if (val is IEnumerable && !(val is string))
            {
                var concatValue = string.Empty;
                var paramArray = val as IEnumerable;

                return paramArray.Cast<object>().Aggregate(concatValue, (current, paramValue) => current + (paramValue + ";"));
            }

            return val.ToString();
        }
    }
}