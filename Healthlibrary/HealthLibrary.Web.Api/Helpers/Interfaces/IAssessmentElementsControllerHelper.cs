using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.AssessmentElements;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// Provides help methods to manage Assessment elements.
    /// </summary>
    public interface IAssessmentElementsControllerHelper
    {
        /// <summary>
        /// Returns response model for request to get Assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        Task<AssessmentResponseDto> GetById(int customerId, Guid elementId);

        /// <summary>
        /// Returns list of Assessment elements models for specified customer.
        /// Copies data from CI customer to specified if required customer didn't have any Assessment elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<AssessmentResponseDto>> GetAll(int customerId, TagsSearchDto request);
    }
}
