using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models.AssessmentMedias;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// Contains help methods for assessment media controller.
    /// </summary>
    public class AssessmentMediaControllerHelper : IAssessmentMediaControllerHelper
    {
        private readonly IAssessmentMediaService assessmentMediaService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentMediaControllerHelper"/> class.
        /// </summary>
        /// <param name="assessmentMediaService">The assessment media service.</param>
        public AssessmentMediaControllerHelper(IAssessmentMediaService assessmentMediaService)
        {
            this.assessmentMediaService = assessmentMediaService;
        }

        /// <summary>
        /// Creates new instance of AssessmentMedia using request model.
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Guid> CreateAssessmentMedia(int customerId, Guid patientId, CreateAssessmentMediaRequestDto request)
        {
            var entity = Mapper.Map<AssessmentMedia>(request);
            entity.PatientId = patientId;
            entity.CustomerId = customerId;

            var result = await this.assessmentMediaService.Create(entity);

            return result.Id;
        }

        /// <summary>
        /// Saves file content for specified media.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="assessmentMediaId"></param>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public async Task<OperationResultDto<AssessmentMedia, UpdateAssessmentMediaStatus>> UpdateAssessmentMedia(
            int customerId, Guid patientId, Guid assessmentMediaId, HttpContent fileContent)
        {
            var fileBytes = await fileContent.ReadAsByteArrayAsync();

            var result = await this.assessmentMediaService.Update(customerId, patientId, assessmentMediaId, fileBytes);

            return result;
        }

        /// <summary>
        /// Returns required assessment media by identifier.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <param name="assessmentMediaId"></param>
        /// <returns></returns>
        public async Task<AssessmentMediaResponseDto> GetAssessmentMediaById(int customerId, Guid patientId,
            Guid assessmentMediaId)
        {
            var assessmentMedia = await this.assessmentMediaService.GetById(customerId, patientId, assessmentMediaId);

            return Mapper.Map<AssessmentMediaResponseDto>(assessmentMedia);
        }

        /// <summary>
        /// Returns all assessment media records for specified patient.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patientId"></param>
        /// <returns></returns>
        public async Task<PagedResult<AssessmentMediaResponseDto>> GetAllAssessmentMedia(int customerId, Guid patientId,
            AssessmentMediaSearchDto searchDto)
        {
            var searchResult = await this.assessmentMediaService.GetAll(customerId, patientId, searchDto);

            return new PagedResult<AssessmentMediaResponseDto>
            {
                Results = Mapper.Map<IList<AssessmentMediaResponseDto>>(searchResult.Results),
                Total = searchResult.Total
            };
        }
    }
}