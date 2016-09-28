using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CustomerService.Common.Extensions;
using CustomerService.Domain.Dtos;
using CustomerService.Domain.Dtos.Enums;
using CustomerService.Web.Api.CustomAttributes;
using CustomerService.Web.Api.Filters;
using CustomerService.Web.Api.Helpers.Interfaces;
using CustomerService.Web.Api.Models.Dtos;
using CustomerService.Web.Api.Models.Dtos.Enums;
using CustomerService.Web.Api.Models.Dtos.Organizations;
using Swashbuckle.Swagger.Annotations;

namespace CustomerService.Web.Api.Controllers
{
    /// <summary>
    /// OrganizationsController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/organizations")]
    public class OrganizationsController : ApiController
    {
        private readonly IOrganizationsControllerHelper organizationsControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController" /> class.
        /// </summary>
        /// <param name="organizationsControllerHelper">The organizations controller helper.</param>
        public OrganizationsController(IOrganizationsControllerHelper organizationsControllerHelper)
        {
            this.organizationsControllerHelper = organizationsControllerHelper;
        }

        /// <summary>
        /// Gets the organizations.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<OrganizationResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with customer organizations.", typeof(PagedResultDto<OrganizationResponseDto>))]
        public async Task<IHttpActionResult> GetOrganizations(
            int customerId,
            [FromUri]OrganizationSearchDto request
        )
        {
            var response = await organizationsControllerHelper.GetOrganizations(customerId, request);

            return Ok(response);
        }

        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{organizationId:guid}")]
        [ResponseType(typeof(OrganizationResponseDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing customer organization.", typeof(OrganizationResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer organization with such identifier does not exist.")]
        public async Task<IHttpActionResult> GetOrganization(
            int customerId,
            Guid organizationId
        )
        {
            var result = await organizationsControllerHelper.GetOrganization(customerId, organizationId);

            if (result.Status.HasFlag(OrganizationStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = result.Status.Description()
                    }
                );
            }

            return Ok(result.Content);
        }

        /// <summary>
        /// Creates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        public async Task<IHttpActionResult> CreateOrganization(
            int customerId,
            CreateOrganizationRequestDto request
        )
        {
            var result = await organizationsControllerHelper.CreateOrganization(customerId, request);

            if (!result.Status.HasFlag(OrganizationStatus.Success))
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
                new PostResponseDto<Guid> { Id = result.Content }
            );
        }

        /// <summary>
        /// Updates the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{organizationId:guid}")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [SwaggerResponse(HttpStatusCode.OK, "Customer organization was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer organization does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        public async Task<IHttpActionResult> UpdateOrganization(
            int customerId,
            Guid organizationId,
            UpdateOrganizationRequestDto request
        )
        {
            var status = await organizationsControllerHelper.UpdateOrganization(customerId, organizationId, request);

            if (!status.HasFlag(OrganizationStatus.Success))
            {
                return Content(
                    HttpStatusCode.BadRequest,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.GetConcatString()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Deletes the organization.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{organizationId:guid}")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [SwaggerResponse(HttpStatusCode.OK, "Customer organization was archived.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Customer organization with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        public async Task<IHttpActionResult> DeleteOrganization(
            int customerId,
            Guid organizationId
        )
        {
            var status = await organizationsControllerHelper.DeleteOrganization(customerId, organizationId);

            if (status.HasFlag(OrganizationStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.Description()
                    }
                );
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}