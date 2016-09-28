using System;
using System.Threading.Tasks;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.MeasurementElements;

namespace HealthLibrary.Web.Api.Helpers.Interfaces
{
    /// <summary>
    /// IMeasurementElementsHelper.
    /// </summary>
    public interface IMeasurementElementsControllerHelper
    {
        /// <summary>
        /// Returns response model for request to get measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        Task<MeasurementResponseDto> GetById(int customerId, Guid elementId);

        /// <summary>
        /// Returns list of measurement elements models for specified customer.
        /// Copies data from CI customer to specified if required customer didn't have any measurement elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<PagedResultDto<MeasurementResponseDto>> GetAll(int customerId, TagsSearchDto request);
    }
}