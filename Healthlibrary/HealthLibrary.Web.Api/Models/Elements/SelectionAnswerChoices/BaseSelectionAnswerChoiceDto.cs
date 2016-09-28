namespace HealthLibrary.Web.Api.Models.Elements.SelectionAnswerChoices
{
    /// <summary>
    /// Contains common fields for SelectionAnswerChoiceModel.
    /// </summary>
    public abstract class BaseSelectionAnswerChoiceDto
    {
        /// <summary>
        /// Open-ended/closed-ended flag
        /// </summary>
        public bool IsOpenEnded { get; set; }
    }
}