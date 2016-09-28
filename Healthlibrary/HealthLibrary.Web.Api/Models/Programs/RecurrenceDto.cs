using System;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// RecurrenceDto.
    /// </summary>
    public class RecurrenceDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        [Required]
        [Range(0, Int32.MaxValue, ErrorMessageResourceName = "RecurrenceDto_StartDay_ValidationError", ErrorMessageResourceType = typeof(GlobalStrings))]
        [LessThanOrEqualTo("EndDay")]
        public int StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        [Required]
        [Range(0, Int32.MaxValue, ErrorMessageResourceName = "RecurrenceDto_EndDay_ValidationError", ErrorMessageResourceType = typeof(GlobalStrings))]
        [GreaterThanOrEqualTo("StartDay")]
        public int EndDay { get; set; }

        /// <summary>
        /// Gets or sets the every days.
        /// </summary>
        /// <value>
        /// The every days.
        /// </value>
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessageResourceName = "RecurrenceDto_EveryDays_ValidationError", ErrorMessageResourceType = typeof(GlobalStrings))]
        public int EveryDays { get; set; }
    }
}