using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using HealthLibrary.Common;
using HealthLibrary.Domain.Dtos.TokenService;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Exceptions;
using HealthLibrary.Web.Api.Extensions;
using Microsoft.Practices.ServiceLocation;
using JWT;
using NLog;

namespace HealthLibrary.Web.Api
{
    /// <summary>
    /// Provides info about care element context for current request.
    /// </summary>
    public class CareElementRequestContext : ICareElementRequestContext
    {
        private const string HealthLibraryServiceName = "HealthLibrary";
        private const string CustomerIdRouteKey = "customerId";
        private const string LanguageRouteKey = "language";

        private readonly ITokenService tokenService;
        private readonly int ciCustomerId;

        private string controllerName;
        private bool? isCIUser;
        private bool? isAuthorizedRequest;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Returns instance of care element context for current request.
        /// </summary>
        public static ICareElementContext Current
        {
            get { return ServiceLocator.Current.GetInstance<ICareElementContext>(); }
        }

        /// <summary>
        /// Returns value of customer id specified in request.
        /// </summary>
        public int CustomerId { get; private set; }

        /// <summary>
        /// Returns language which should be used in context of current request.
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Returns language which should be used by default.
        /// </summary>
        public string DefaultLanguage { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CareElementRequestContext"/> class.
        /// </summary>
        public CareElementRequestContext(ITokenService tokenService)
        {
            this.tokenService = tokenService;

            var routeData = HttpContext.Current.Request.RequestContext.RouteData.GetWebApiRouteData();

            this.CustomerId = ReadCustomerId(routeData);
            this.Language = ReadLanguage(routeData);
            // If customer id specified then default language for customer should be loaded.
            this.DefaultLanguage = Settings.DefaultLanguage;
            this.ciCustomerId = Settings.CICustomerId;
        }

        /// <summary>
        /// Adds required data to context to allow use
        /// authorization checks.
        /// </summary>
        /// <param name="controllerName"></param>
        /// <exception cref="System.ArgumentNullException">controllerName</exception>
        /// <exception cref="ArgumentNullException">controllerName</exception>
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
                throw new Exception("Initialize method must be executed before usage authorization checks.");
            }

            isCIUser = await this.HasAccess(ciCustomerId);

            return isCIUser.Value;
        }

        /// <summary>
        /// Verifies if user who send request is to current action.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsAuthorizedRequest()
        {
            if (isAuthorizedRequest.HasValue)
            {
                return isCIUser.Value;
            }

            if (string.IsNullOrEmpty(this.controllerName))
            {
                throw new Exception("Initialize method must be executed before usage authorization checks.");
            }

            isAuthorizedRequest = await this.HasAccess(this.CustomerId);

            return isAuthorizedRequest.Value;
        }

        /// <summary>
        /// Returns container name for blob storage
        /// based on content type (customer or generic).
        /// </summary>
        /// <returns></returns>
        public string GetMediaContainerName()
        {
            string containerNamePrefix = ConfigurationManager.AppSettings["BlobStorageContainerNamePrefix"];

            return string.Format("{0}-{1}", containerNamePrefix, this.CustomerId).ToLower();
        }

        #region Private methods

        /// <summary>
        /// Verifies if token from request has access to specified customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        private async Task<bool> HasAccess(int customerId)
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
                Service = HealthLibraryServiceName,
                Action = action,
                Controller = controller,
                Customer = customerId,
                Id = token
            };

            var parts = token.Split('.');

            if (parts.Length == 3)
            {
                return VerifyJwtLocal(token, request);
            }
            else
            {
                return await this.tokenService.VerifyAccess(request);
            }
        }

        private static bool VerifyJwtLocal(string token, VerifyTokenRequest request)
        {
            var jwtCustomerKey = ConfigurationManager.AppSettings["JWT_KEY_" + request.Customer.Value] as string;

            if (jwtCustomerKey == null)
            {
                logger.Fatal("JSON Web Token (JWT) was used but [JWT_KEY_{0}] is not set.", request.Customer);
                return false;
            }

            // Local JWT token
            var payload = new Dictionary<string, string>();

            try
            {
                payload = JsonWebToken.DecodeToObject<Dictionary<string, string>>(token, jwtCustomerKey, true);
            }
            catch (Exception e)
            {
                logger.Error(e, "Error decoding JWT: {0}", token);
            }

            var customer = Convert.ToInt32(payload["customer"]);

            if (request.Customer.Value != customer)
            {
                logger.Error("Requested customer {0} does not match token customer {1}.", request.Customer, customer);
                return false;
            }

            if (!request.Service.Equals(payload["service"], StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error("Requested service {0} does not match token service {1}.", request.Service, payload["service"]);
                return false;

            }

            if (!request.Controller.Equals(payload["controller"], StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error("Requested controller {0} does not match token controller {1}.", request.Service, payload["controller"]);
                return false;
            }

            if (!request.Action.Equals(payload["action"], StringComparison.InvariantCultureIgnoreCase))
            {
                logger.Error("Requested action {0} does not match token action {1}.", request.Service, payload["action"]);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reads customer Id from route data.
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        private static int ReadCustomerId(IDictionary<string, object> routeData)
        {
            object customerIdRouteValue;
            int customerId;

            if (!routeData.TryGetValue(CustomerIdRouteKey, out customerIdRouteValue) ||
                !int.TryParse(customerIdRouteValue.ToString(), out customerId))
            {
                throw new ContextUsageException("CareElementRequestContext requires customerId specified in request.");
            }

            return customerId;
        }

        /// <summary>
        /// Reads language from route data.
        /// </summary>
        /// <param name="routeData"></param>
        /// <returns></returns>
        private static string ReadLanguage(IDictionary<string, object> routeData)
        {
            object languageRouteValue;

            if (!routeData.TryGetValue(LanguageRouteKey, out languageRouteValue))
            {
                return null;
            }

            return languageRouteValue.ToString();
        }

        #endregion
    }
}