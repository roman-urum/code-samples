using System;

namespace DeviceService.Web.Api.Filters
{
    /// <summary>
    /// TokenAuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TokenAuthorizeAttribute : Attribute
    {
    }
}