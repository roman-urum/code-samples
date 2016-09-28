using System.Web;
using DeviceService.Common;
using DeviceService.Web.Api.Extensions;

namespace DeviceService.Web.Api
{
    /// <summary>
    /// CustomerContext.
    /// </summary>
    public class CustomerContext : ICustomerContext
    {
        private const string CustomerIdRouteKey = "customerId";
        
        /// <summary>
        /// Returns value of customer specified in the request.
        /// </summary>
        public int? CustomerId {
            get
            {
                var webApiRouteData = HttpContext.Current.Request.RequestContext.RouteData.GetWebApiRouteData();

                object customerIdRouteValue;

                if (webApiRouteData.TryGetValue(CustomerIdRouteKey, out customerIdRouteValue))
                {
                    int customerId;

                    if (int.TryParse(customerIdRouteValue.ToString(), out customerId))
                    {
                        return customerId;
                    }
                }

                return null;
            }
        }
    }
}