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

namespace Maestro.DataAccess.Api.DataProviders.Interfaces
{
    /// <summary>
    /// IHealthLibraryDataProvider.
    /// </summary>
    public partial interface IHealthLibraryDataProvider
    {
        #region Question elements

        /// <summary>
        /// Loads list of selection answer sets from health library.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<QuestionElementResponseDto>> FindQuestionElements(SearchRequestDto request, string token);

        Task<PostResponseDto<Guid>> CreateQuestion(CreateQuestionElementRequestDto createQuestionDto, int customerId, string token);

        /// <summary>
        /// Updates the question element.
        /// </summary>
        /// <param name="updateQuestionElementDto">The update question element dto.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task UpdateQuestionElement(UpdateQuestionElementRequestDto updateQuestionElementDto, int customerId, string token);

        Task<QuestionElementResponseDto> GetQuestionElement(string token, int customerId, Guid id, bool isBrief);

        #endregion

        #region Selection Answer Sets

        Task<IList<SelectionAnswerSetResponseDto>> FindSelectionAnswerSets(SearchSelectionRequestDto request, string token);

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
        /// Updates the selection answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task UpdateSelectionAnswerSet(UpdateSelectionAnswerSetRequestDto request, int customerId, string token);

        #endregion

        #region Scale answer sets

        /// <summary>
        /// Loads scale answer sets from health library.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<ScaleAnswerSetResponseDto>> FindScaleAnswerSets(SearchRequestDto request, string token);

        /// <summary>
        /// Gets the scale answer set.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<ScaleAnswerSetResponseDto> GetScaleAnswerSet(Guid id, int customerId, string token);

        /// <summary>
        /// Posts the scale answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId"></param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> PostScaleAnswerSet(CreateScaleAnswerSetRequestDto request, int customerId, string token);

        /// <summary>
        /// Updates the scale answer set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        Task UpdateScaleAnswerSet(UpdateScaleAnswerSetRequestDto request, int customerId, string token);

        #endregion

        #region Open Ended Answer Sets

        /// <summary>
        /// Gets the open ended answer set.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<OpenEndedAnswerSetResponseDto> GetOpenEndedAnswerSet(int customerId, string token);

        #endregion

        #region Text Media Elements

        /// <summary>
        /// Returns list of text media elements which matches to specified search criteria.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<TextMediaElementResponseDto>> FindTextMediaElements(SearchRequestDto searchRequest,
            string token);

        /// <summary>
        /// Creates the text media element.
        /// </summary>
        /// <param name="createTextMediaElementDto">The create text media element dto.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<PostResponseDto<Guid>> CreateTextMediaElement(CreateTextMediaElementRequestDto createTextMediaElementDto, int customerId, string token);

        /// <summary>
        /// Updates the text media element.
        /// </summary>
        /// <param name="updateTextMediaElementDto">The update text media element dto.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task UpdateTextMediaElement(UpdateTextMediaElementRequestDto updateTextMediaElementDto, int customerId, string token);

        Task<TextMediaElementResponseDto> GetTextMediaElement(string token, int customerId, Guid id);

        #endregion

        #region Media elements

        /// <summary>
        /// Creates the media element.
        /// </summary>
        /// <param name="mediaDto">The media dto.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<Guid> CreateMediaElement(CreateMediaRequestDto mediaDto, int customerId, string token);

        /// <summary>
        /// Gets the media element.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="mediaId">The media identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<MediaResponseDto> GetMediaElement(int customerId, Guid mediaId, string token);

        Task UpdateMediaElement(UpdateMediaRequestDto updateMediaDto, int customerId, string token);

        Task<IList<MediaResponseDto>> FindMediaElements(SearchMediaDto searchRequestDto, int customerId, string token);

        #endregion

        #region Measurement Elements

        /// <summary>
        /// Returns list of measurement elements for specified customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IList<MeasurementResponseDto>> GetMeasurementElements(int customerId, string token);

        /// <summary>
        /// Returns details of measurement element by id.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="measurementId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<MeasurementResponseDto> GetMeasurementElement(int customerId, Guid measurementId, string token);

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
    }
}