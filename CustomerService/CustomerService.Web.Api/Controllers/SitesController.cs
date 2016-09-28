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
using CustomerService.Web.Api.Models.Dtos.Site;
using Swashbuckle.Swagger.Annotations;

namespace CustomerService.Web.Api.Controllers
{
    /// <summary>
    /// SitesController.
    /// </summary>
    [TokenAuthorize]
    [RoutePrefix(@"api/{customerId:regex(^[1-9]\d*)}/sites")]
    public class SitesController : ApiController
    {
        private readonly ISitesControllerHelper sitesHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="SitesController"/> class.
        /// </summary>
        /// <param name="sitesHelper">The site service.</param>
        public SitesController(ISitesControllerHelper sitesHelper)
        {
            this.sitesHelper = sitesHelper;
        }

        /// <summary>
        /// Gets the customer sites.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(PagedResultDto<SiteResponseDto>))]
        public async Task<IHttpActionResult> GetSites(
            int customerId,
            [FromUri]SiteSearchDto request
        )
        {
            var responseModel = await sitesHelper.GetSites(customerId, request);

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets site by specified identifier.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{siteId:guid}")]
        [ResponseType(typeof(SiteResponseDto))]
        public async Task<IHttpActionResult> GetSite(
            int customerId,
            Guid siteId
        )
        {
            var responseModel = await sitesHelper.GetSite(customerId, siteId);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = SiteStatus.NotFound.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets site by name.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteName">Name of the site.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("name/{siteName}")]
        [ResponseType(typeof(SiteResponseDto))]
        public async Task<IHttpActionResult> GetSiteByName(
            int customerId,
            string siteName
        )
        {
            var responseModel = await sitesHelper.GetSiteByName(customerId, siteName);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = SiteStatus.NotFound.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets site by site NPI.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteNpi">The site NPI.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"npi/{siteNpi:regex(^[a-zA-Z0-9\-]+$)}")]
        [ResponseType(typeof(SiteResponseDto))]
        public async Task<IHttpActionResult> GetSiteByNpi(
            int customerId,
            string siteNpi
        )
        {
            var responseModel = await sitesHelper
                .GetSiteBySiteNpi(customerId, siteNpi);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = SiteStatus.NotFound.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Gets site by customer site id.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="customerSiteId">The customer site Id.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"customer-site-id/{customerSiteId:regex(^[a-zA-Z0-9\-]+$)}")]
        [ResponseType(typeof(SiteResponseDto))]
        public async Task<IHttpActionResult> GetSiteByCustomerSiteId(
            int customerId,
            string customerSiteId
        )
        {
            var responseModel = await sitesHelper
                .GetSiteByCustomerSiteId(customerId, customerSiteId);

            if (responseModel == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = SiteStatus.NotFound.Description()
                    }
                );
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Creates new site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        public async Task<IHttpActionResult> CreateSite(
            int customerId,
            CreateSiteRequestDto model
        )
        {
            var result = await sitesHelper.CreateSite(customerId, model);

            if (!result.Status.HasFlag(SiteStatus.Success))
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
                new PostResponseDto<Guid>() { Id = result.Content }
            );
        }

        /// <summary>
        /// Updates site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route("{siteId:guid}")]
        [HttpPut]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        public async Task<IHttpActionResult> UpdateSite(
            int customerId,
            Guid siteId,
            UpdateSiteRequestDto model
        )
        {
            var status = await sitesHelper.UpdateSite(customerId, siteId, model);

            if (status.HasFlag(SiteStatus.NotFound))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = SiteStatus.NotFound.Description()
                    }
                );
            }

            if (!status.HasFlag(SiteStatus.Success))
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
        /// Deletes the site.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{siteId:guid}")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [SwaggerResponse(HttpStatusCode.OK, "Site was archived.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Site with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        public async Task<IHttpActionResult> DeleteSite(
            int customerId,
            Guid siteId
        )
        {
            var result = await sitesHelper.DeleteSite(customerId, siteId);

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