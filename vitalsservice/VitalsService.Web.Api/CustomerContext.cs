using System.Configuration;
using System.Web;
using VitalsService.Web.Api.Extensions;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// CustomerContext.
    /// </summary>
    public class CustomerContext : ICustomerContext
    {
        private const string CustomerIdRouteKey = "customerId";
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerContext"/> class.
        /// </summary>
        public CustomerContext()
        {
            var routeData = HttpContext.Current.Request.RequestContext.RouteData.GetWebApiRouteData();
            object customerIdRouteValue;
            int customerId;

            if (!routeData.TryGetValue(CustomerIdRouteKey, out customerIdRouteValue) ||
                !int.TryParse(customerIdRouteValue.ToString(), out customerId))
            {
                return;
            }

            this.CustomerId = customerId;
        }

        /// <summary>
        /// Returns value of customer specified in request.
        /// </summary>
        public int CustomerId { get; private set; }

        /// <summary>
        /// Returns container name for blob storage
        /// for current customer.
        /// </summary>
        /// <returns></returns>
        public string GetMediaContainerName()
        {
            string containerNamePrefix = ConfigurationManager.AppSettings["BlobStorageContainerNamePrefix"];

            return string.Format("{0}-{1}", containerNamePrefix, this.CustomerId).ToLower();
        }
    }
}