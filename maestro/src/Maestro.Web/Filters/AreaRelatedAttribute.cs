using System;
using System.Web.Mvc;
using Maestro.Web.Extensions;

namespace Maestro.Web.Filters
{
    /// <summary>
    /// Decorator to execute attributes as global but only for specified area.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AreaRelatedAttribute : ActionFilterAttribute
    {
        private readonly ActionFilterAttribute _attributeToExecute;
        private readonly string _area;

        public AreaRelatedAttribute(string area, ActionFilterAttribute attributeToExecute)
        {
            if (attributeToExecute == null)
            {
                throw new ArgumentNullException("attributeToExecute");
            }

            if (string.IsNullOrEmpty(area))
            {
                throw new ArgumentNullException("area");
            }

            _area = area;
            _attributeToExecute = attributeToExecute;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.IsAreaMatches(filterContext))
            {
                _attributeToExecute.OnActionExecuting(filterContext);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (this.IsAreaMatches(filterContext))
            {
                _attributeToExecute.OnActionExecuted(filterContext);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (this.IsAreaMatches(filterContext))
            {
                _attributeToExecute.OnResultExecuted(filterContext);
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (this.IsAreaMatches(filterContext))
            {
                _attributeToExecute.OnResultExecuting(filterContext);
            }
        }

        /// <summary>
        /// Identifies that child attribute can be executed.
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool IsAreaMatches(ControllerContext filterContext)
        {
            var currentArea = filterContext.HttpContext.Request.RequestContext.RouteData.GetArea();

            return !string.IsNullOrEmpty(currentArea) && currentArea.Equals(_area);
        }
    }
}