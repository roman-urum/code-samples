using VitalsService.Domain.Enums;
namespace VitalsService.Web.Api.Models.Alerts
{
    /// <summary>
    /// VitalAlertBriefResponseDto. This class was created to be used in VitalBriefResponseDto class. 
    /// We cannot use BaseAlertResponse in VitalBriefResponseDto class because of circular referencing.
    /// </summary>
    public class VitalAlertBriefResponseDto : BaseAlertResponseDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public VitalType Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public UnitType Unit { get; set; }

        /// <summary>
        /// Gets or sets the violated threshold.
        /// </summary>
        /// <value>
        /// The violated threshold.
        /// </value>
        public ViolatedThresholdDto ViolatedThreshold { get; set; }
    }
}