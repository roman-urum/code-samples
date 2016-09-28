using System.Web;
using System.Web.Mvc;
using Maestro.Common.Extensions;
using Maestro.Domain.Dtos.CustomerService;
using Maestro.DomainLogic.Services.Interfaces;
using Maestro.Web.Extensions;
using Maestro.Web.Security;

namespace Maestro.Web
{
    /// <summary>
    /// Static container for ICustomerContextManager entity.
    /// </summary>
    public static class CustomerContext
    {
        public const string CustomerAreaName = "Customer";
        public const string CustomerSubdomainRouteName = "subdomain";
        private const string CustomerAreaPathTemplate = "{0}/{1}";

        /// <summary>
        /// Returns instance for current customer context.
        /// </summary>
        public static ICustomerContext Current
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomerContext>();
            }
        }

        /// <summary>
        /// Returns path to customer area for current request.
        /// </summary>
        public static string AreaPath
        {
            get
            {
                return CustomerAreaPathTemplate.FormatWith(CustomerAreaName,
                    new HttpContextWrapper(HttpContext.Current).Request.GetCustomerSubdomain());
            }
        }
    }

    /// <summary>
    /// Interface to provide access to currect sustomer (specified in route).
    /// </summary>
    public interface ICustomerContext
    {
        /// <summary>
        /// Contains instance of current customer for area.
        /// </summary>
        CustomerResponseDto Customer { get; }
    }

    public class DefaultCustomerContext : ICustomerContext
    {
        /// <summary>
        /// Contains instance of current customer for area.
        /// </summary>
        public CustomerResponseDto Customer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCustomerContext"/> class.
        /// </summary>
        /// <param name="customersService">The customers service.</param>
        /// <param name="authDataStorage">The authentication data storage.</param>
        public DefaultCustomerContext(
            ICustomersService customersService,
            IAuthDataStorage authDataStorage
        )
        {
            var context = new HttpContextWrapper(HttpContext.Current);
            string subdomain = context.Request.GetCustomerSubdomain();

            if (string.IsNullOrEmpty(subdomain))
            {
                return;
            }

            var authData = authDataStorage.GetUserAuthData();

            if (authData == null)
            {
                return;
            }

            Customer = customersService.GetCustomer(authData.CustomerId, subdomain, authData.Token);
        }
    }
}