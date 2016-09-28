using Maestro.Common.ApiClient;

namespace Maestro.Domain.Dtos.HealthLibraryService.Elements.SelectionAnswerSet
{
    /// <summary>
    /// SearchSelectionRequestDto.
    /// </summary>
    public class SearchSelectionRequestDto : SearchRequestDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is multiple choice.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is multiple choice; otherwise, <c>false</c>.
        /// </value>
        [RequestParameter(RequestParameterType.RequestBody)]
        public bool IsMultipleChoice { get; set; }
    }
}