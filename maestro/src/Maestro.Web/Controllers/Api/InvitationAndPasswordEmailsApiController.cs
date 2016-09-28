using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Filters;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;

namespace Maestro.Web.Controllers.Api
{
    /// <summary>
    /// InvitationAndPasswordEmailsApiController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/customer-user")]
    public class InvitationAndPasswordEmailsApiController : ApiController
    {
        private readonly ICustomerUsersManager customerUsersManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsersApiController"/> class.
        /// </summary>
        /// <param name="customerUsersManager">The customer users manager.</param>
        public InvitationAndPasswordEmailsApiController(ICustomerUsersManager customerUsersManager)
        {
            this.customerUsersManager = customerUsersManager;
        }

        /// <summary>
        /// Requests the email invitation.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("request-email-invitation")]
        public async Task<IHttpActionResult> RequestEmailInvitation(
            int customerId,
            SendEmailInvitationRequestDto request
        )
        {
            await customerUsersManager.RequestEmailInvitation(customerId, request.Email);

            // Don't reveal that the customer user with such email doesn't exist or something similar,
            // always return success
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Requests the password reset.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("request-password-reset")]
        public async Task<IHttpActionResult> RequestPasswordReset(
            int customerId,
            SendResetPasswordEmailRequestDto request
        )
        {
            await customerUsersManager.RequestPasswordReset(customerId, request.Email);

            // Don't reveal that the customer user with such email doesn't exist or something similar,
            // always return success
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}