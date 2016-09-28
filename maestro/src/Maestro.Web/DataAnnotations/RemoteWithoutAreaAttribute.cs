using System;
using System.Web.Mvc;

namespace Maestro.Web.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RemoteWithoutAreaAttribute : RemoteAttribute
    {
        public RemoteWithoutAreaAttribute(string action, string controller)
            : base(action, controller)
        {
            this.RouteData["area"] = string.Empty;
        }
    }
}