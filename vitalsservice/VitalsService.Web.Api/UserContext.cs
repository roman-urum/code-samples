using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using VitalsService.Domain.Dtos.TokenServiceDtos;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Extensions;

namespace VitalsService.Web.Api
{
    /// <summary>
    /// Provides info about authorized user.
    /// </summary>
    public class UserContext : IUserContext
    {
        private const int DefaultCICustomerId = 1;
        private const string VitalsServiceName = "vitals";

        private readonly int ciCustomerId;
        private readonly ITokenService tokenService;

        private string controllerName;
        private bool? isCIUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContext"/> class.
        /// </summary>
        /// <param name="tokenService">The token service.</param>
        public UserContext(ITokenService tokenService)
        {
            this.tokenService = tokenService;

            var ciCustomerIdValue = ConfigurationManager.AppSettings["CICustomerId"];

            if (string.IsNullOrEmpty(ciCustomerIdValue) || !int.TryParse(ciCustomerIdValue, out this.ciCustomerId))
            {
                this.ciCustomerId = DefaultCICustomerId;
            }
        }

        /// <summary>
        /// Adds required data to context to allow use
        /// authorization checks.
        /// </summary>
        /// <param name="controllerName"></param>
        public void Initialize(string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName))
            {
                throw new ArgumentNullException("controllerName");
            }

            this.controllerName = controllerName;
        }

        /// <summary>
        /// Verifies if user who send request is care innovations admin.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCIUser()
        {
            if (isCIUser.HasValue)
            {
                return isCIUser.Value;
            }

            if (string.IsNullOrEmpty(this.controllerName))
            {
                return false;
            }

            isCIUser = await this.HasAccess(ciCustomerId);

            return isCIUser.Value;
        }

        /// <summary>
        /// Verifies if token from request has access to specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<bool> HasAccess(int? customerId)
        {
            var httpRequest = HttpContext.Current.Request;

            var action = HttpContext.Current.Request.HttpMethod;
            var controller = controllerName;
            var token = httpRequest.GetAuthToken();

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var request = new VerifyTokenRequest
            {
                Service = VitalsServiceName,
                Action = action,
                Controller = controller,
                Customer = customerId,
                Id = token
            };

            return await this.tokenService.CheckAccess(request);
        }
    }
}