using System;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.MeasurementElements;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// Provides help methods to manage measurement elements.
    /// </summary>
    public class MeasurementElementsControllerHelper : IMeasurementElementsControllerHelper
    {
        private readonly IMeasurementElementsService measurementElementsService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MeasurementElementsControllerHelper"/> class.
        /// </summary>
        /// <param name="measurementElementsService">The measurement elements service.</param>
        public MeasurementElementsControllerHelper(IMeasurementElementsService measurementElementsService)
        {
            this.measurementElementsService = measurementElementsService;
        }

        /// <summary>
        /// Returns response model for request to get measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public async Task<MeasurementResponseDto> GetById(int customerId, Guid elementId)
        {
            var result = await this.measurementElementsService.GetById(customerId, elementId);

            if (result == null)
            {
                return null;
            }

            return Mapper.Map<MeasurementResponseDto>(result);
        }

        /// <summary>
        /// Returns list of measurement elements models for specified customer.
        /// Copies data from CI customer to specified if required customer didn't have any measurement elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<MeasurementResponseDto>> GetAll(int customerId, TagsSearchDto request)
        {
            var result = await measurementElementsService.GetAll(customerId, request);

            return Mapper.Map<PagedResult<MeasurementElement>, PagedResultDto<MeasurementResponseDto>>(result);
        }
    }
}