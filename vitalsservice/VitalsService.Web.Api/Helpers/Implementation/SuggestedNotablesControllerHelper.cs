using System;
using System.Threading.Tasks;
using AutoMapper;
using VitalsService.Domain.DbEntities;
using VitalsService.Domain.Dtos;
using VitalsService.Domain.Enums;
using VitalsService.DomainLogic.Services.Interfaces;
using VitalsService.Web.Api.Helpers.Interfaces;
using VitalsService.Web.Api.Models;
using VitalsService.Web.Api.Models.PatientNotes;

namespace VitalsService.Web.Api.Helpers.Implementation
{
    /// <summary>
    /// NotablesControllerHelper.
    /// </summary>
    public class SuggestedNotablesControllerHelper : ISuggestedNotablesControllerHelper
    {
        private readonly IPatientNotesService patientNotesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestedNotablesControllerHelper"/> class.
        /// </summary>
        /// <param name="patientNotesService">The patient notes service.</param>
        public SuggestedNotablesControllerHelper(IPatientNotesService patientNotesService)
        {
            this.patientNotesService = patientNotesService;
        }

        /// <summary>
        /// Creates the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<Guid, SuggestedNotableStatus>> CreateSuggestedNotable(
            int customerId, 
            SuggestedNotableRequestDto request
        )
        {
            var suggestedNotable = Mapper.Map<SuggestedNotableRequestDto, SuggestedNotable>(request);
            suggestedNotable.CustomerId = customerId;

            return await patientNotesService.CreateSuggestedNotable(suggestedNotable);
        }

        /// <summary>
        /// Updates the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<SuggestedNotableStatus> UpdateSuggestedNotable(
            int customerId,
            Guid suggestedNotableId, 
            SuggestedNotableRequestDto request
        )
        {
            var suggestedNotable = Mapper.Map<SuggestedNotableRequestDto, SuggestedNotable>(request);
            suggestedNotable.Id = suggestedNotableId;
            suggestedNotable.CustomerId = customerId;

            return await patientNotesService.UpdateSuggestedNotable(suggestedNotable);
        }

        /// <summary>
        /// Deletes the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        public Task<SuggestedNotableStatus> DeleteSuggestedNotable(
            int customerId, 
            Guid suggestedNotableId
        )
        {
            return patientNotesService.DeleteSuggestedNotable(customerId, suggestedNotableId);
        }

        /// <summary>
        /// Gets the suggested notable.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="suggestedNotableId">The suggested notable identifier.</param>
        /// <returns></returns>
        public async Task<OperationResultDto<SuggestedNotableDto, SuggestedNotableStatus>> GetSuggestedNotable(
            int customerId,
            Guid suggestedNotableId
        )
        {
            var result = await patientNotesService.GetSuggestedNotable(customerId, suggestedNotableId);

            if (result == null)
            {
                return await Task.FromResult(
                    new OperationResultDto<SuggestedNotableDto, SuggestedNotableStatus>()
                    {
                        Status = SuggestedNotableStatus.NotFound
                    }
                );
            }

            return await Task.FromResult(
                new OperationResultDto<SuggestedNotableDto, SuggestedNotableStatus>()
                {
                    Status = SuggestedNotableStatus.Success,
                    Content = Mapper.Map<SuggestedNotable, SuggestedNotableDto>(result)
                }
            );
        }

        /// <summary>
        /// Gets the suggested notables.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<PagedResultDto<SuggestedNotableDto>> GetSuggestedNotables(
            int customerId,
            BaseSearchDto request
        )
        {
            var result = await patientNotesService.GetSuggestedNotables(customerId, request);

            return Mapper.Map<PagedResult<SuggestedNotable>, PagedResultDto<SuggestedNotableDto>>(result);
        }
    }
}