using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Maestro.Common.Extensions;
using Maestro.Domain.Dtos;
using Maestro.Web.Areas.Customer.Managers.Interfaces;
using Maestro.Web.Filters;
using Maestro.Web.Models.Api.Dtos.Entities;
using Maestro.Web.Models.Api.Dtos.Enums;
using Maestro.Web.Models.Api.Dtos.RequestsResponses;

namespace Maestro.Web.Controllers.Api
{
    /// <summary>
    /// CustomerUsersApiController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/customer-user")]
    public class CustomerUsersApiController : ApiController
    {
        private readonly ICustomerUsersManager customerUsersManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerUsersApiController"/> class.
        /// </summary>
        /// <param name="customerUsersManager">The customer users manager.</param>
        public CustomerUsersApiController(ICustomerUsersManager customerUsersManager)
        {
            this.customerUsersManager = customerUsersManager;
        }

        /// <summary>
        /// Creates the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateCustomerUser(
            int customerId,
            CreateCustomerUserRequestDto request
        )
        {
            var result = await customerUsersManager.CreateCustomerUser(customerId, request);

            if (!result.IsValid)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Error.GetConcatString()
                    });
            }

            return Created(
                new Uri(Request.RequestUri, result.Id.ToString()),
                new PostResponseDto<Guid> { Id = result.Id }
            );
        }

        /// <summary>
        /// Updates the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> UpdateCustomerUser(
            int customerId,
            Guid id,
            UpdateCustomerUserRequestDto request
        )
        {
            var result = await customerUsersManager.UpdateCustomerUser(customerId, id, request);

            if (!result.IsValid)
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Error.GetConcatString()
                    });
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the customer user.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteCustomerUser(
            int customerId,
            Guid id
        )
        {
            bool result = await customerUsersManager.DeleteCustomerUser(customerId, id);

            if (!result)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerUserDoesNotExist.Description()
                    });
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Gets the customer user by customer user identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerUserId">The customer user identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"customer-user-id/{customerUserId:regex(^[a-zA-Z0-9\-]+$)}")]
        [ResponseType(typeof(CustomerUserDto))]
        public async Task<IHttpActionResult> GetCustomerUserByCustomerUserId(
            int customerId,
            string customerUserId
        )
        {
            var responseModel = 
                await customerUsersManager.GetCustomerUserByCustomerUserId(customerId, customerUserId);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerUserWithSuchCustomerUserIdDoesNotExist.Description()
                    });
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets the customer user by npi.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="npi">The npi.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"npi/{npi:regex(^[a-zA-Z0-9\-]+$)}")]
        [ResponseType(typeof(CustomerUserDto))]
        public async Task<IHttpActionResult> GetCustomerUserByNpi(
            int customerId,
            string npi
        )
        {
            var responseModel = await customerUsersManager.GetCustomerUserByNpi(customerId, npi);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerUserWithSuchNPIDoesNotExist.Description()
                    });
            }

            return Ok(responseModel);
        }
    }
}