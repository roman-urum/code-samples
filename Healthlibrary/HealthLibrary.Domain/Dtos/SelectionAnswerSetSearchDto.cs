namespace HealthLibrary.Domain.Dtos
{
    /// <summary>
    /// Contains criteria to search selection answersets.
    /// </summary>
    public class SelectionAnswerSetSearchDto : TagsSearchDto
    {
        /// <summary>
        /// Type of answer set: multiple choice or single selection.
        /// </summary>
        public bool? IsMultipleChoice { get; set; }
    }
}