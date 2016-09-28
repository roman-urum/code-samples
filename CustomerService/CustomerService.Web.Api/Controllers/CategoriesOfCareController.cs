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
using CustomerService.Web.Api.Models.Dtos.CategoryOfCare;
using CustomerService.Web.Api.Models.Dtos.Enums;
using Swashbuckle.Swagger.Annotations;

namespace CustomerService.Web.Api.Controllers
{
    /// <summary>
    /// CategoriesOfCareController.
    /// </summary>
    [TokenAuthorize]
    public class CategoriesOfCareController : ApiController
    {
        private readonly ICategoriesOfCareControllerHelper categoriesOfCareControllerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesOfCareController"/> class.
        /// </summary>
        /// <param name="categoriesOfCareControllerHelper">The categories of cares controller helper.</param>
        public CategoriesOfCareController(
            ICategoriesOfCareControllerHelper categoriesOfCareControllerHelper
        )
        {
            this.categoriesOfCareControllerHelper = categoriesOfCareControllerHelper;
        }

        /// <summary>
        /// Creates the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/categories-of-care")]
        [ResponseType(typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.Created, "New category of care was created.", typeof(PostResponseDto<Guid>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Invalid parameter value(s).")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [InvalidateCacheOutput("GetSites", typeof(SitesController))]
        [InvalidateCacheOutput("GetSite", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByName", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByNpi", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByCustomerSiteId", typeof(SitesController))]
        public async Task<IHttpActionResult> CreateCategoryOfCare(
            int customerId,
            CategoryOfCareRequestDto request
        )
        {
            var result = await categoriesOfCareControllerHelper.CreateCategoryOfCare(customerId, request);

            if (!result.Status.HasFlag(CategoryOfCareStatus.Success))
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
        /// Updates the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/categories-of-care/{categoryOfCareId:guid}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Category of care was updated.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category of care does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [InvalidateCacheOutput("GetSites", typeof(SitesController))]
        [InvalidateCacheOutput("GetSite", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByName", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByNpi", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByCustomerSiteId", typeof(SitesController))]
        public async Task<IHttpActionResult> UpdateCategoryOfCare(
            int customerId,
            Guid categoryOfCareId,
            CategoryOfCareRequestDto request
        )
        {
            var status = await categoriesOfCareControllerHelper.UpdateCategoryOfCare(categoryOfCareId, request);

            if (status.HasFlag(CategoryOfCareStatus.CategoryOfCareWithSuchIdDoesNotExist))
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = status.GetConcatString()
                    }
                );
            }

            if (!status.HasFlag(CategoryOfCareStatus.Success))
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
        /// Deletes the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/categories-of-care/{categoryOfCareId:guid}")]
        [SwaggerResponse(HttpStatusCode.NoContent, "Category of care was deleted.")]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category of care with such identifier does not exist.")]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Incorrent attributes provided in the request.")]
        [InvalidateCacheOutput("GetCustomers", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomer", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerByName", typeof(CustomersController))]
        [InvalidateCacheOutput("GetCustomerBySubdomain", typeof(CustomersController))]
        [InvalidateCacheOutput("GetSites", typeof(SitesController))]
        [InvalidateCacheOutput("GetSite", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByName", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByNpi", typeof(SitesController))]
        [InvalidateCacheOutput("GetSiteByCustomerSiteId", typeof(SitesController))]
        public async Task<IHttpActionResult> DeleteCategoryOfCare(
            int customerId,
            Guid categoryOfCareId
        )
        {
            var status = await categoriesOfCareControllerHelper.DeleteCategoryOfCare(categoryOfCareId);

            if (status == CategoryOfCareStatus.CategoryOfCareWithSuchIdDoesNotExist)
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

        /// <summary>
        /// Gets the category of care.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="categoryOfCareId">The category of care identifier.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/categories-of-care/{categoryOfCareId:guid}")]
        [ResponseType(typeof(CategoryOfCareResponseDto))]
        [SwaggerResponse(HttpStatusCode.OK, "Existing category of care.", typeof(CategoryOfCareResponseDto))]
        [SwaggerResponse(HttpStatusCode.NotFound, "Category of care does not exist.")]
        public async Task<IHttpActionResult> GetCategoryOfCare(
            int customerId,
            Guid categoryOfCareId
        )
        {
            var result = await categoriesOfCareControllerHelper.GetCategoryOfCare(categoryOfCareId);

            if (result == null)
            {
                return Content(
                    HttpStatusCode.NotFound,
                    new ErrorResponseDto()
                    {
                        Error = ErrorCode.InvalidRequest,
                        Message = ErrorCode.InvalidRequest.Description(),
                        Details = CategoryOfCareStatus.CategoryOfCareWithSuchIdDoesNotExist.Description()
                    }
                );
            }

            return Ok(result);
        }

        /// <summary>
        /// Gets the categories of cares.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpGet]
        [Route(@"api/{customerId:regex(^[1-9]\d*)}/categories-of-care")]
        [ResponseType(typeof(PagedResultDto<CategoryOfCareResponseDto>))]
        [SwaggerResponse(HttpStatusCode.OK, "Response with сategories of care.", typeof(PagedResultDto<CategoryOfCareResponseDto>))]
        public async Task<IHttpActionResult> GetCategoriesOfCare(
            int customerId,
            [FromUri]BaseSearchDto request
        )
        {
            var result = await categoriesOfCareControllerHelper.GetCategoriesOfCare(customerId, request);

            return Ok(result);
        }
    }
}