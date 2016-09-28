using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HealthLibrary.Domain.Constants;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.QuestionElements
{
    /// <summary>
    /// Contains base properties for question element dtos.
    /// </summary>
    public abstract class BaseQuestionElementRequestDto
    {
        /// <summary>
        /// Reference to answer set which should be used to answer on question.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public Guid AnswerSetId { get; set; }

        /// <summary>
        /// Answer set tags.
        /// </summary>
        [ElementStringLength(
            30,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        [UniqueStringsList(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "UniqueStringsListAttribute_ValidationError",
            ErrorMessage = null
            )]
        [ListRegularExpression(
            ValidationExpressions.Tag,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "TagsValidationMessage",
            ErrorMessage = null
        )]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// External question id.
        /// </summary>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string ExternalId { get; set; }

        /// <summary>
        /// Internal question id.
        /// </summary>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string InternalId { get; set; }

        /// <summary>
        /// Ids for answer choices in context of question.
        /// </summary>
        public IList<AnswerChoiceIdDto> AnswerChoiceIds { get; set; }
    }
}