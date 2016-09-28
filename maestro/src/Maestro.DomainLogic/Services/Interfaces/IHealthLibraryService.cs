using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Maestro.Domain.Dtos;
using Maestro.Domain.Dtos.HealthLibraryService.Elements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.AssessmentElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.MeasurementElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.Medias;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.QuestionElements;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.ScaleAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet;
using Maestro.Domain.Dtos.HealthLibraryService.Elements.TextMediaElements;

namespace Maestro.DomainLogic.Services.Interfaces
{
    /// <summary>
    /// IHealthLibraryService.
    /// </summary>
    public partial interface IHealthLibraryService
    {
        /// <summary>
        /// Returns list of scale answer sets matches to search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<QuestionElementResponseDto>> FindQuestionElements(SearchRequestDto searchRequest, string token);

        /// <summary>
        /// Creates ScaleAnswerSet.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateScaleAnswerSet(CreateScaleAnswerSetRequestDto request, int customerId, string token);

        /// <summary>
        /// Returns list of scale answer sets matches to search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<ScaleAnswerSetResponseDto>> FindScaleAnswerSets(SearchRequestDto searchRequest, string token);

        /// <summary>
        /// Returns list of selection answer sets mathes to search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<SelectionAnswerSetResponseDto>> FindSelectionAnswerSets(SearchSelectionRequestDto searchRequest, string token);

        /// <summary>
        /// Creates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateSelectionAnswerSet(CreateSelectionAnswerSetRequestDto request, int customerId, string token);

        /// <summary>
        /// Gets the selection answer set.
        /// </summary>
        /// <param name="selectionAnswerSetId">The selection answer set identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<SelectionAnswerSetResponseDto> GetSelectionAnswerSet(Guid selectionAnswerSetId, int customerId, string token);

        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<OpenEndedAnswerSetResponseDto> GetOpenEndedAnswerSet(int customerId, string token);

        /// <summary>
        /// Creates the question.
        /// </summary>
        /// <param name="createQuestionDto">The create question dto.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateQuestion(CreateQuestionElementRequestDto createQuestionDto, int customerId, string token);

        /// <summary>
        /// Updates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task UpdateSelectionAnswerSet(UpdateSelectionAnswerSetRequestDto request, int customerId, string token);

        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<ScaleAnswerSetResponseDto> GetScaleAnswerSet(Guid id, int customerId, string token);

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        Task UpdateScaleAnswerSet(UpdateScaleAnswerSetRequestDto model, int id, string token);

        #region Text Media Elements

        /// <summary>
        /// Returns list of text media elements which matches to specified search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<TextMediaElementResponseDto>> FindTextMediaElements(
            SearchRequestDto searchRequest, string token);

        /// <summary>
        /// Creates the text and media element.
        /// </summary>
        /// <param name="createTextMediaElementDto">The create text media element dto.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateTextAndMediaElement(CreateTextMediaElementRequestDto createTextMediaElementDto, int customerId, string token);

        #endregion


        Task UpdateQuestionElement(UpdateQuestionElementRequestDto updateQuestionElementDto, int customerId, string token);

        Task UpdateTextMediaElement(UpdateTextMediaElementRequestDto updateTextMediaElementDto, int customerId, string token);

        Task<Guid> CreateMediaElement(CreateMediaRequestDto mediaDto, int customerId, string token);

        #region Measurement elements

        /// <summary>
        /// Returns list of measurement elements
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<MeasurementResponseDto>> GetMeasurementElements(int customerId, string token);

        /// <summary>
        /// Returns element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="elementId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<MeasurementResponseDto> GetMeasurementElement(int customerId, Guid elementId, string token);

        #endregion

        #region Assessment Elements

        /// <summary>
        /// Returns list of assessment elements for specified customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IList<AssessmentResponseDto>> GetAssessmentElements(int customerId, string token);

        /// <summary>
        /// Returns details of assessment element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="assessmentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<AssessmentResponseDto> GetAssessmentElement(int customerId, Guid assessmentId, string token);

        #endregion

        Task<MediaResponseDto> GetMediaElement(int customerId, Guid mediaId, string token);

        Task<TextMediaElementResponseDto> GetTextMediaElement(string token, int customerId, Guid id);

        Task<QuestionElementResponseDto> GetQuestionElement(string token, int customerId, Guid id, bool isBrief);

        Task UpdateMediaElement(UpdateMediaRequestDto updateMediaDto, int customerId, string token);

        Task<IList<MediaResponseDto>> FindMediaElements(SearchMediaDto searchRequestDto, int customerId, string token);
    }
}