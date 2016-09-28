using System;
using System.Collections.Generic;
using Maestro.Domain.Dtos.HealthLibraryService.Enums;
using Maestro.Web.Areas.Customer.Models.CareBuilder.LocalizedStrings;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.QuestionElements
{
    /// <summary>
    /// Dto to load info about question element.
    /// </summary>
    public class QuestionElementListItemViewModel
    {
        /// <summary>
        /// Id of question element in health library.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of assigned answerset.
        /// </summary>
        public Guid AnswerSetId { get; set; }

        /// <summary>
        /// Question CI id.
        /// </summary>
        public string InternalId { get; set; }

        /// <summary>
        /// Question external id.
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// Type of assigned answerset.
        /// </summary>
        public AnswerSetType AnswerSetType { get; set; }

        /// <summary>
        /// Question translate.
        /// </summary>
        public BaseLocalizedStringViewModel QuestionElementString { get; set; }

        /// <summary>
        /// List of tags assigned to question.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        /// Contains list of internal and external ids for answers.
        /// </summary>
        public List<AnswerChoiceIdsViewModel> AnswerChoiceIds { get; set; }
    }
}