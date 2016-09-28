using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maestro.DataAccess.Api.DataProviders.Interfaces;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.AssessmentElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.MeasurementElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.TextMediaElements;
using Maestro.DomainLogic.Services.Interfaces;
using RestSharp;

namespace Maestro.DomainLogic.Services.Implementations
{
    /// <summary>
    /// HealthLibraryService.
    /// </summary>
    public partial class HealthLibraryService : IHealthLibraryService
    {
        private readonly IHealthLibraryDataProvider healthLibraryDataProvider;

        public HealthLibraryService(IHealthLibraryDataProvider healthLibraryDataProvider)
        {
            this.healthLibraryDataProvider = healthLibraryDataProvider;
        }

        #region Questin Element

        /// <summary>
        /// Returns list of scale answer sets matches to search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<QuestionElementResponseDto>> FindQuestionElements(SearchRequestDto searchRequest, string token)
        {
            return await this.healthLibraryDataProvider.FindQuestionElements(searchRequest, token);
        }

        /// <summary>
        /// Sends request to health library to create new question element.
        /// </summary>
        /// <param name="createQuestionDto"></param>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateQuestion(CreateQuestionElementRequestDto createQuestionDto,
            int customerId, string token)
        {
            createQuestionDto.AnswerChoiceIds = createQuestionDto.AnswerChoiceIds.Where(a => !a.IsEmpty()).ToList();

            return await this.healthLibraryDataProvider.CreateQuestion(createQuestionDto, customerId, token);
        }

        /// <summary>
        /// Updates data of existed question element.
        /// </summary>
        /// <param name="updateQuestionElementDto"></param>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateQuestionElement(UpdateQuestionElementRequestDto updateQuestionElementDto, int customerId,
            string token)
        {
            updateQuestionElementDto.AnswerChoiceIds =
                updateQuestionElementDto.AnswerChoiceIds.Where(a => !a.IsEmpty()).ToList();

            await this.healthLibraryDataProvider.UpdateQuestionElement(updateQuestionElementDto, customerId, token);
        }

        #endregion

        #region Scale Answer Set

        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<ScaleAnswerSetResponseDto> GetScaleAnswerSet(Guid id, int customerId, string token)
        {
            return await healthLibraryDataProvider.GetScaleAnswerSet(id, customerId, token);
        }

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        public async Task UpdateScaleAnswerSet(UpdateScaleAnswerSetRequestDto request, int customerId, string token)
        {
            await healthLibraryDataProvider.UpdateScaleAnswerSet(request, customerId, token);
        }

        /// <summary>
        /// Creates ScaleAnswerSet.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateScaleAnswerSet(CreateScaleAnswerSetRequestDto request, int customerId, string token)
        {
            return await healthLibraryDataProvider.PostScaleAnswerSet(request, customerId, token);
        }

        /// <summary>
        /// Returns list of scale answer sets matches to search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<ScaleAnswerSetResponseDto>> FindScaleAnswerSets(SearchRequestDto searchRequest, string token)
        {
            return await this.healthLibraryDataProvider.FindScaleAnswerSets(searchRequest, token);
        }

        #endregion

        #region Selection Answer Set

        /// <summary>
        /// Returns list of scale answer sets matches to search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<SelectionAnswerSetResponseDto>> FindSelectionAnswerSets(SearchSelectionRequestDto searchRequest, string token)
        {
            return await this.healthLibraryDataProvider.FindSelectionAnswerSets(searchRequest, token);
        }

        /// <summary>
        /// Creates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> CreateSelectionAnswerSet(CreateSelectionAnswerSetRequestDto request, int customerId, string token)
        {
            return await healthLibraryDataProvider.CreateSelectionAnswerSet(request, customerId, token);
        }

        /// <summary>
        /// Gets the selection answer set.
        /// </summary>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<SelectionAnswerSetResponseDto> GetSelectionAnswerSet(Guid selectionAnswerSetId, int customerId, string token)
        {
            return await healthLibraryDataProvider.GetSelectionAnswerSet(selectionAnswerSetId, customerId, token);
        }

        /// <summary>
        /// Updates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public Task UpdateSelectionAnswerSet(UpdateSelectionAnswerSetRequestDto request, int customerId, string token)
        {
            return healthLibraryDataProvider.UpdateSelectionAnswerSet(request, customerId, token);
        }

        #endregion

        #region Open Ended Answer Set

        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<OpenEndedAnswerSetResponseDto> GetOpenEndedAnswerSet(int customerId, string token)
        {
            return await this.healthLibraryDataProvider.GetOpenEndedAnswerSet(customerId, token);
        }

        #endregion

        #region Text Media Elements

        /// <summary>
        /// Returns list of text media elements which matches to specified search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TextMediaElementResponseDto>> FindTextMediaElements(
            SearchRequestDto searchRequest, string token)
        {
            return await this.healthLibraryDataProvider.FindTextMediaElements(searchRequest, token);
        }

        public async Task<PostResponseDto<Guid>> CreateTextAndMediaElement(CreateTextMediaElementRequestDto createTextMediaElementDto, int customerId, string token)
        {
            return await healthLibraryDataProvider.CreateTextMediaElement(createTextMediaElementDto, customerId, token);
        }

        public async Task UpdateTextMediaElement(UpdateTextMediaElementRequestDto updateTextMediaElementDto, int customerId, string token)
        {
            await this.healthLibraryDataProvider.UpdateTextMediaElement(updateTextMediaElementDto, customerId, token);
        }

        public async Task<Guid> CreateMediaElement(CreateMediaRequestDto mediaDto, int customerId, string token)
        {
            return await this.healthLibraryDataProvider.CreateMediaElement(mediaDto, customerId, token);
        }

        #endregion

        #region Measurement elements

        /// <summary>
        /// Returns list of measurement elements
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<MeasurementResponseDto>> GetMeasurementElements(int customerId, string token)
        {
            return await this.healthLibraryDataProvider.GetMeasurementElements(customerId, token);
        }

        /// <summary>
        /// Returns element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MeasurementResponseDto> GetMeasurementElement(int customerId, Guid elementId, string token)
        {
            return await this.healthLibraryDataProvider.GetMeasurementElement(customerId, elementId, token);
        }

        public async Task<MediaResponseDto> GetMediaElement(int customerId, Guid mediaId, string token)
        {
            return await this.healthLibraryDataProvider.GetMediaElement(customerId, mediaId, token);
        }

        public async Task<TextMediaElementResponseDto> GetTextMediaElement(string token, int customerId, Guid id)
        {
            return await this.healthLibraryDataProvider.GetTextMediaElement(token, customerId, id);
        }

        public async Task<QuestionElementResponseDto> GetQuestionElement(string token, int customerId, Guid id, bool isBrief)
        {
            return await this.healthLibraryDataProvider.GetQuestionElement(token, customerId, id, isBrief);
        }

        public async Task UpdateMediaElement(UpdateMediaRequestDto updateMediaDto, int customerId, string token)
        {
            await this.healthLibraryDataProvider.UpdateMediaElement(updateMediaDto, customerId, token);
        }

        public async Task<IList<MediaResponseDto>> FindMediaElements(SearchMediaDto searchRequestDto, int customerId, string token)
        {
            return await this.healthLibraryDataProvider.FindMediaElements(searchRequestDto, customerId, token);
        }

        #endregion

        #region Assessment Elements

        /// <summary>
        /// Returns list of assessment elements for specified customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IList<AssessmentResponseDto>> GetAssessmentElements(int customerId, string token)
        {
            return await this.healthLibraryDataProvider.GetAssessmentElements(customerId, token);
        }

        /// <summary>
        /// Returns details of assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="assessmentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<AssessmentResponseDto> GetAssessmentElement(int customerId, Guid assessmentId, string token)
        {
            return await this.healthLibraryDataProvider.GetAssessmentElement(customerId, assessmentId, token);
        }

        #endregion
    }
}
