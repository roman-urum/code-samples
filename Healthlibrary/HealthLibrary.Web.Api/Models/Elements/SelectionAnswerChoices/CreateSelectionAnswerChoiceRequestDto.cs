namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// Model of answer from answer set.
    /// </summary>
    public class CreateSelectionAnswerChoiceRequestDto : BaseSelectionAnswerChoiceDto
    {
        /// <summary>
        /// Default string for answer choice.
        /// </summary>
        public CreateAnswerStringRequestDto AnswerString { get; set; }
    }
}