namespace Maestro.Web.Areas.Customer.Models.CareBuilder.SelectionAnswerSet
{
    /// <summary>
    /// BaseSelectionAnswerSetDto.
    /// </summary>
    public abstract class BaseSelectionAnswerSetViewModel : AnswerSetViewModel
    {
        /// <summary>
        /// Type of answer selection.
        /// </summary>
        public bool IsMultipleChoice { get; set; }
    }
}