using System.ComponentModel.DataAnnotations;
using FoolproofWebApi;
using Maestro.Web.Resources;

namespace Maestro.Web.Areas.Customer.Models.CareBuilder.ScaleAnsweSet
{
    public class BaseScaleAnswerSetViewModel : AnswerSetViewModel
    {

        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>
        /// The low value.
        /// </value>
        [Required]
        [Range(0, 100, ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_LowValue_Bounds")]
        [LessThan("HighValue", ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_LowValue_Low_value_must_be_less_then_high_value")]
        public int LowValue { get; set; }
        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>
        /// The high value.
        /// </value>
        [Required]
        [Range(0, 100, ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_HighValue_Bounds")]
        [GreaterThan("LowValue", ErrorMessageResourceType = typeof(GlobalStrings),
            ErrorMessageResourceName = "BaseScaleAnswerSetDto_HighValue_Greater")]
        public int HighValue { get; set; }
    }
}