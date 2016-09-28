using System;

namespace HealthLibrary.Web.Api.Filters
{
    /// <summary>
    /// TokenAuthorizeAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TokenAuthorizeAttribute : Attribute
    {
    }
}