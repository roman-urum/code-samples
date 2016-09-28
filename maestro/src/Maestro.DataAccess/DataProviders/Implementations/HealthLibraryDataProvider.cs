using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Common;
using Maestro.DataAccess.Api.ApiClient;
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
using RestSharp;

namespace Maestro.DataAccess.Api.DataProviders.Implementations
{
    /// <summary>
    /// HealthLibraryDataProvider.
    /// </summary>
    public partial class HealthLibraryDataProvider : IHealthLibraryDataProvider
    {
        private readonly IRestApiClient apiClient;

        public HealthLibraryDataProvider(IRestApiClientFactory apiClientFactory)
        {
            this.apiClient = apiClientFactory.Create(Settings.HealthLibraryServiceUrl);
        }

        #region Question Elements

        public async Task<PostResponseDto<Guid>> CreateQuestion(CreateQuestionElementRequestDto createQuestionDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/question-elements", customerId);

            return await this.apiClient.SendRequestAsync<PostResponseDto<Guid>>(url, createQuestionDto, Method.POST, null, token);

        }

        public async Task UpdateQuestionElement(UpdateQuestionElementRequestDto updateQuestionElementDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/question-elements/{1}", customerId, updateQuestionElementDto.Id);

            await this.apiClient.SendRequestAsync(url, updateQuestionElementDto, Method.PUT, null, token);
        }

        /// <summary>
        /// Loads list of selection answer sets from health library.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<QuestionElementResponseDto>> FindQuestionElements(
            SearchRequestDto request,
            string token
        )
        {
            var pagedResult = await apiClient
                .SendRequestAsync<PagedResult<QuestionElementResponseDto>>(
                    "/api/{CustomerId}/question-elements",
                    request, 
                    Method.GET,
                    null,
                    token
                );

            return pagedResult.Results;
        }

        public async Task<QuestionElementResponseDto> GetQuestionElement(string token, int customerId, Guid id, bool isBrief)
        {
            string url = string.Format("/api/{0}/question-elements/{1}?isBrief={2}", customerId, id, isBrief);

            return await this.apiClient.SendRequestAsync<QuestionElementResponseDto>(url, null, Method.GET, null, token);
        }

        #endregion

        #region Scale

        /// <summary>
        /// Posts the scale answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId"></param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<PostResponseDto<Guid>> PostScaleAnswerSet(CreateScaleAnswerSetRequestDto request, int customerId, string token)
        {
            var url = string.Format("api/{0}/answer-sets/scale", customerId);

            return await apiClient.SendRequestAsync<PostResponseDto<Guid>>(
                url, request, Method.POST, null, token);
        }

        /// <summary>
        /// Loads list of scale answer sets from health library.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<ScaleAnswerSetResponseDto>> FindScaleAnswerSets(SearchRequestDto request,
            string token)
        {
            var url = string.Format("/api/{0}/answer-sets/scale/", request.CustomerId);

            var pagedResult =  await this.apiClient.SendRequestAsync<PagedResult<ScaleAnswerSetResponseDto>>(
                url, request, Method.GET, null, token);

            return pagedResult.Results;
        }

        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<ScaleAnswerSetResponseDto> GetScaleAnswerSet(Guid id, int customerId, string token)
        {
            var url = string.Format("api/{0}/answer-sets/scale/{1}", customerId, id);

            return await apiClient.SendRequestAsync<ScaleAnswerSetResponseDto>(url, null, Method.GET, null, token);
        }

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        public async Task UpdateScaleAnswerSet(UpdateScaleAnswerSetRequestDto request, int customerId, string token)
        {
            var url = string.Format("api/{0}/answer-sets/scale/{1}", customerId, request.Id);

            await apiClient.SendRequestAsync(url, request, Method.PUT, null, token);
        }

        #endregion

        #region Selection Answer Sets

        /// <summary>
        /// Loads list of selection answer sets from health library.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<SelectionAnswerSetResponseDto>> FindSelectionAnswerSets(SearchSelectionRequestDto request, string token)
        {
            var url = string.Format("/api/{0}/answer-sets/selection", request.CustomerId);

            var pagedResult = await this.apiClient.SendRequestAsync<PagedResult<SelectionAnswerSetResponseDto>>(
                url, request, Method.GET, null, token);

            return pagedResult.Results;
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
            var url = string.Format("api/{0}/answer-sets/selection", customerId);

            return await apiClient.SendRequestAsync<PostResponseDto<Guid>>(url, request, Method.POST, null, token);
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
            var url = string.Format("api/{0}/answer-sets/selection/{1}", customerId, selectionAnswerSetId);

            return await apiClient.SendRequestAsync<SelectionAnswerSetResponseDto>(url, null, Method.GET, null, token);
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
            var url = string.Format("api/{0}/answer-sets/selection/{1}", customerId, request.Id);

            return apiClient.SendRequestAsync(url, request, Method.PUT, null, token);
        }

        #endregion

        #region Open Ended Answer Sets

        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<OpenEndedAnswerSetResponseDto> GetOpenEndedAnswerSet(int customerId, string token)
        {
            var url = string.Format("api/{0}/answer-sets/open-ended/", customerId);

            return await apiClient.SendRequestAsync<OpenEndedAnswerSetResponseDto>(url, null, Method.GET, null, token);
        }

        #endregion

        #region Text Media Elements

        public async Task UpdateTextMediaElement(UpdateTextMediaElementRequestDto updateTextMediaElementDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/text-media-elements/{1}", customerId, updateTextMediaElementDto.Id);

            await this.apiClient.SendRequestAsync(url, updateTextMediaElementDto, Method.PUT, null, token);
        }

        /// <summary>
        /// Returns list of text media elements which matches to specified search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TextMediaElementResponseDto>> FindTextMediaElements(
            SearchRequestDto searchRequest, string token)
        {
            var pagedResult = await
                this.apiClient.SendRequestAsync<PagedResult<TextMediaElementResponseDto>>(
                    "/api/{CustomerId}/text-media-elements", searchRequest, Method.GET, null, token);

            return pagedResult.Results;
        }

        public async Task<PostResponseDto<Guid>> CreateTextMediaElement(CreateTextMediaElementRequestDto createTextMediaElementDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/text-media-elements", customerId);

            return await this.apiClient.SendRequestAsync<PostResponseDto<Guid>>(url, createTextMediaElementDto, Method.POST, null, token);
        }

        public async Task<TextMediaElementResponseDto> GetTextMediaElement(string token, int customerId, Guid id)
        {
            string url = string.Format("/api/{0}/text-media-elements/{1}", customerId, id);

            return await this.apiClient.SendRequestAsync<TextMediaElementResponseDto>(url, null, Method.GET, null, token);
        }

        #endregion

        #region Measurement Elements

        /// <summary>
        /// Returns list of measurement elements for specified customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public async Task<IList<MeasurementResponseDto>> GetMeasurementElements(int customerId, string token)
        {
            var url = string.Format("/api/{0}/measurement-elements", customerId);

            var pagedResult = await this.apiClient.SendRequestAsync<PagedResult<MeasurementResponseDto>>(url, null, Method.GET, null, token);

            return pagedResult.Results;
        }

        /// <summary>
        /// Returns details of measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="measurementId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MeasurementResponseDto> GetMeasurementElement(int customerId, Guid measurementId, string token)
        {
            var url = string.Format("/api/{0}/measurement-elements/{1}", customerId, measurementId);

            return await this.apiClient.SendRequestAsync<MeasurementResponseDto>(url, null, Method.GET, null, token);
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
            var url = string.Format("/api/{0}/measurement-elements", customerId);

            var pagedResult = await this.apiClient.SendRequestAsync<PagedResult<AssessmentResponseDto>>(url, null, Method.GET, null, token);

            return pagedResult.Results;
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
            var url = string.Format("/api/{0}/assessment-elements/{1}", customerId, assessmentId);

            return await this.apiClient.SendRequestAsync<AssessmentResponseDto>(url, null, Method.GET, null, token);
        }

        #endregion

        #region Media

        public async Task<Guid> CreateMediaElement(CreateMediaRequestDto mediaDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/medias", customerId);

            var createMediaResponse = await this.apiClient.SendRequestAsync<MediaResponseDto>(url, mediaDto, Method.POST, null, token);

            return createMediaResponse.Id;
        }

        public async Task<MediaResponseDto> GetMediaElement(int customerId, Guid mediaId, string token)
        {
            var url = string.Format("/api/{0}/medias/{1}", customerId, mediaId);

            return await this.apiClient.SendRequestAsync<MediaResponseDto>(url, null, Method.GET, null, token);
        }

        public async Task UpdateMediaElement(UpdateMediaRequestDto updateMediaDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/medias/{1}", customerId, updateMediaDto.Id);

            await this.apiClient.SendRequestAsync<MediaResponseDto>(url, updateMediaDto, Method.PUT, null, token);
        }

        public async Task<IList<MediaResponseDto>> FindMediaElements(SearchMediaDto searchRequestDto, int customerId, string token)
        {
            var url = string.Format("/api/{0}/medias?", customerId);
            var pagedResult = await this.apiClient.SendRequestAsync<PagedResult<MediaResponseDto>>(url, searchRequestDto, Method.GET, null, token);

            return pagedResult.Results;
        }

        #endregion
    }
}
