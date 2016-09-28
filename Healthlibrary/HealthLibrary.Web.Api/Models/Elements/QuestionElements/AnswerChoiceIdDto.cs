using System;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Elements.QuestionElements
{
    /// <summary>
    /// Dto to specify ids for selection answer choice.
    /// </summary>
    public class AnswerChoiceIdDto
    {
        /// <summary>
        /// Id of selection answer choice.
        /// Required if value not specified.
        /// </summary>
        [RequiredIfEmpty("Value")]
        public Guid? Id { get; set; }

        /// <summary>
        /// Value of scale answer choice.
        /// Required if id not specified.
        /// </summary>
        [RequiredIfEmpty("Id")]
        public int? Value { get; set; }

        /// <summary>
        /// Internal answer choice id.
        /// </summary>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string InternalId { get; set; }

        /// <summary>
        /// External answer choice id.
        /// </summary>
        [StringLength(
            100,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "StringLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string ExternalId { get; set; }

        /// <summary>
        /// Internal answer choice score.
        /// </summary>
        [Range(
            0,
            int.MaxValue,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RangeAttribute_ValidationError"
        )]
        public int? InternalScore { get; set; }

        /// <summary>
        /// External answer choice score.
        /// </summary>
        [Range(
            0,
            int.MaxValue,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RangeAttribute_ValidationError"
        )]
        public int? ExternalScore { get; set; }
    }
}