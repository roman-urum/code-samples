using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerService.Common.Extensions;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Web.Api.CustomAttributes;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Customer;
using CustomerService.Web.Api.Models.Dtos.Enums;
using Swashbuckle.Swagger.Annotations;

namespace CustomerService.Web.Api.Controllers
{
    /// <summary>
    /// Customer Controller. Get/Post/Put/Delete actions.
    /// </summary>
    [TokenAuthorize]
    public class CustomersController : ApiController
    {
        private readonly ICustomersControllerHelper customersHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="customersHelper">The customer service.</param>
        public CustomersController(ICustomersControllerHelper customersHelper)
        {
            this.customersHelper = customersHelper;
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="includeArchived">if set to <c>true</c> [include archived].</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/customers")]
        [ResponseType(typeof(PagedResultDto<FullCustomerResponseDto>))]
        public async Task<IHttpActionResult> GetCustomers(
            string q = null,
            int? skip = null,
            int? take = null,
            bool includeArchived = false,
            bool isBrief = true
        )
        {
            // Temporary workaround to avoid unsuccessfull request validation
            var request = new CustomersSearchDto
            {
                Q = q ?? string.Empty,
                IncludeArchived = includeArchived
            };
            
            request.Skip = skip ?? request.Skip;
            request.Take = take ?? request.Take;

            var responseModel = await customersHelper.GetCustomers(request, isBrief);

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets the customer by id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/customers/{customerId:regex(^[1-9]\d*)}")]
        [ResponseType(typeof(FullCustomerResponseDto))]
        public async Task<IHttpActionResult> GetCustomer(
            int customerId,
            bool isBrief = true
        )
        {
            var responseModel = await customersHelper.GetCustomer(customerId, isBrief);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerDoesNotExists.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets customer by name.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/customers/name/{name}")]
        [ResponseType(typeof(FullCustomerResponseDto))]
        public async Task<IHttpActionResult> GetCustomerByName(
            int customerId,
            string name,
            bool isBrief = true
        )
        {
            var responseModel = await customersHelper.GetCustomerByName(customerId, name, isBrief);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerDoesNotExists.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets customer by subdomain.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="subdomain">The subdomain.</param>
        /// <param name="isBrief">if set to <c>true</c> [is brief].</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/customers/subdomain/{subdomain}")]
        [ResponseType(typeof(FullCustomerResponseDto))]
        public async Task<IHttpActionResult> GetCustomerBySubdomain(
            int customerId,
            string subdomain,
            bool isBrief = true
        )
        {
            var responseModel = await customersHelper.GetCustomerBySubdomain(customerId, subdomain, isBrief);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerDoesNotExists.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/customers")]
        [ResponseType(typeof(PostResponseDto<int>))]
        public async Task<IHttpActionResult> CreateCustomer(CreateCustomerRequestDto model)
        {
            var result = await customersHelper.CreateCustomer(model);

            if (!result.Status.HasFlag(CustomerStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.GetConcatString()
                    }
                );
            }

            return Created(
                new Uri(Request.RequestUri, result.Content.ToString()),
                new PostResponseDto<int>() { Id = result.Content }
            );
        }

        /// <summary>
        /// Updates the customer with all customer's sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/customers/{customerId:regex(^[1-9]\d*)}")]
        public async Task<IHttpActionResult> UpdateCustomer(
            [FromUri]int customerId,
            UpdateCustomerRequestDto model
        )
        {
            var result = await customersHelper.UpdateCustomer(customerId, model);

            if (result.HasFlag(CustomerStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = CustomerStatus.NotFound.Description()
                    }
                );
            }

            if (!result.HasFlag(CustomerStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/customers/{customerId:regex(^[1-9]\d*)}")]
        [SwaggerResponse(HttpStatusCode.OK, "Customer was archived.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        public async Task<IHttpActionResult> DeleteCustomer(int customerId)
        {
            var result = await customersHelper.DeleteCustomer(customerId);

            if (!result)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = ErrorCode.CustomerDoesNotExists.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}