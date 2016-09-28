using System;
using System.Web;
using LightInject;

namespace DeviceService.Web.Api.Ioc
{
    /// <summary>
    /// Lifetime to save object instance per one http request.
    /// </summary>
    public class PerHttpRequestLifeTime : ILifetime
    {
        private readonly Guid key = Guid.NewGuid();

        public object GetInstance(Func<object> createInstance, Scope scope)
        {
            if (HttpContext.Current.Items[key] == null)
            {
                HttpContext.Current.Items[key] = createInstance();
            }

            return HttpContext.Current.Items[key];
        }
    }
}