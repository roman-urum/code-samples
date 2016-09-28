using System;
using System.Threading.Tasks;
using AutoMapper;
using HealthLibrary.Domain.Dtos;
using HealthLibrary.Domain.Entities.Element;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models;
using HealthLibrary.Web.Api.Models.Elements.AssessmentElements;

namespace HealthLibrary.Web.Api.Helpers.Implementations
{
    /// <summary>
    /// Provides help methods to manage Assessment elements.
    /// </summary>
    public class AssessmentElementsControllerHelper : IAssessmentElementsControllerHelper
    {
        private readonly IAssessmentElementsService AssessmentElementsService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentElementsControllerHelper"/> class.
        /// </summary>
        /// <param name="AssessmentElementsService">The Assessment elements service.</param>
        public AssessmentElementsControllerHelper(IAssessmentElementsService AssessmentElementsService)
        {
            this.AssessmentElementsService = AssessmentElementsService;
        }

        /// <summary>
        /// Returns response model for request to get Assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <returns></returns>
        public async Task<AssessmentResponseDto> GetById(int customerId, Guid elementId)
        {
            var result = await this.AssessmentElementsService.GetById(customerId, elementId);

            if (result == null)
            {
                return null;
            }

            return Mapper.Map<AssessmentResponseDto>(result);
        }

        /// <summary>
        /// Returns list of Assessment elements models for specified customer.
        /// Copies data from CI customer to specified if required customer didn't have any Assessment elements.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<AssessmentResponseDto>> GetAll(int customerId, TagsSearchDto request)
        {
            var result = await AssessmentElementsService.GetAll(customerId, request);

            return Mapper.Map<PagedResult<AssessmentElement>, PagedResultDto<AssessmentResponseDto>>(result);
        }
    }
}