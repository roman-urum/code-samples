using System;
using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using HealthLibrary.Web.Api.DataAnnotations;
using HealthLibrary.Web.Api.Resources;

namespace HealthLibrary.Web.Api.Models.Programs
{
    /// <summary>
    /// Model for request to get program schedule.
    /// </summary>
    public class ProgramScheduleRequestDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramScheduleRequestDto"/> class.
        /// </summary>
        public ProgramScheduleRequestDto()
        {
            StartDay = 1;
        }

        /// <summary>
        /// The calendar date that represents the first day and time of a program.
        /// </summary>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null
        )]
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the start date tz.
        /// </summary>
        /// <value>
        /// The start date tz.
        /// </value>
        [Required(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "RequiredAttribute_ValidationError",
            ErrorMessage = null,
            AllowEmptyStrings = false
        )]
        [IANATimeZone(
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "IANATimeZoneAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string StartDateTz { get; set; }

        /// <summary>
        /// Gets or sets the start day.
        /// </summary>
        /// <value>
        /// The start day.
        /// </value>
        [Range(
            1,
            int.MaxValue,
            ErrorMessageResourceName = "RangeAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessage = null
        )]
        public int? StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end day.
        /// </summary>
        /// <value>
        /// The end day.
        /// </value>
        [Range(
            1,
            int.MaxValue,
            ErrorMessageResourceName = "RangeAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessage = null
        )]
        [GreaterThanOrEqualTo(
            "StartDay",
            PassOnNull = true,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "GreaterThanOrEqualToAttribute_ValidationError",
            ErrorMessage = null
        )]
        public int? EndDay { get; set; }

        /// <summary>
        /// The number of minutes after which the calendar event should expire. 
        /// Optional. Default is null.
        /// </summary>
        [Range(
            1,
            int.MaxValue,
            ErrorMessageResourceName = "RangeAttribute_ValidationError",
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessage = null
        )]
        public int? ExpireMinutes { get; set; }
    }
}