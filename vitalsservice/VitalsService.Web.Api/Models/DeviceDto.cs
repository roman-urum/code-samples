using System.ComponentModel.DataAnnotations;
using VitalsService.Domain.Constants;
using VitalsService.Web.Api.Resources;

namespace VitalsService.Web.Api.Models
{
    /// <summary>
    /// DeviceDto.
    /// </summary>
    public class DeviceDto
    {
        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        [MaxLength(
            DbConstraints.MaxLength.DeviceUniqueIdentifier,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string UniqueIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the battery percent.
        /// </summary>
        /// <value>
        /// The battery percent.
        /// </value>
        public decimal? BatteryPercent { get; set; }

        /// <summary>
        /// Gets or sets the battery millivolts.
        /// </summary>
        /// <value>
        /// The battery millivolts.
        /// </value>
        public int? BatteryMillivolts { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        [MaxLength(
            DbConstraints.MaxLength.DeviceModel,
            ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "MaxLengthAttribute_ValidationError",
            ErrorMessage = null
        )]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }
    }
}